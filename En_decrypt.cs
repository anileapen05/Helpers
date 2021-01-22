using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;

using Feedback_1_0;
using IO_1_0;

namespace Security_1_0
{



	public class En_decrypt
	{
            //unique id
			private string id;

			//logger code
			public Logger logger;

			//parameters passed to the cryptographic service provider
            private CspParameters encrypt_cspp;
            public CspParameters decrypt_cspp;

			//meter for encryption,decryption
            private int encryption_progress_meter;
            private int decryption_progress_meter;

			//asymmetric algorithms
			public RSACryptoServiceProvider rsa_csp;

			//symmetric algorithms
			public RijndaelManaged rijndael_managed;

			//hash generator
			public SHA1Managed sha1_man;

			//sign formatter
            private RSAPKCS1SignatureFormatter rsa_sf;

			//sign deformatter
            private RSAPKCS1SignatureDeformatter rsa_sdf;

			//the KeyContainerName is responsible for the storage of the key on the system
			//not the CspParameters object
			public En_decrypt(string unique_id)
			{

				try
				{
					if (unique_id == null || unique_id == "")
					{
						return;
					}

					id = unique_id;

					logger = new Logger();

					logger.b_enabled = true;

					logger.setup_Log_File(unique_id.ToString() + "_endec_log.txt");

					encrypt_cspp = new CspParameters();

					encrypt_cspp.KeyContainerName = unique_id + "_Encryption_Key";

					decrypt_cspp = new CspParameters();

					decrypt_cspp.KeyContainerName = unique_id + "_Decryption_Key";

					reset_Encryption_Progress_Meter();
					reset_Decryption_Progress_Meter();
				}
				catch (Exception e)
				{
					return;
				}
			}

            ~En_decrypt()
			{
				try
				{
					logger.log_Result(Object_Result.OK, "destructor: ");
				}
				catch (Exception e)
				{
					logger.log_Result(Object_Result.METHOD_MEMBER_FAILED, "destructor: " + e.Message);
				}
			}



			public void setup_RSA_Decryption()
			{
				try
				{
					//create a new decryption key pair
					rsa_csp = new RSACryptoServiceProvider(decrypt_cspp);
				}
				catch (Exception e)
				{
					logger.log_Result(Object_Result.MEMBER_INVALID, "setup_RSA_Decryption: " + e.Message);
				}
			}

			public void setup_RSA_Encryption(string ext_public_key)
			{
				try
				{
					if (ext_public_key == null || ext_public_key == "")
					{
						logger.log_Result(Object_Result.ARGUMENT_INVALID, "setup_RSA_Encryption: ext_public_key");
						return;
					}

					//create a new encryption key using external public key
					rsa_csp = new RSACryptoServiceProvider(encrypt_cspp);

					rsa_csp.FromXmlString(ext_public_key);

				}
				catch (Exception e)
				{
					logger.log_Result(Object_Result.METHOD_MEMBER_FAILED, "setup_RSA_Encryption: " + e.Message);
				}

			}

			public void lock_RSA_Key_In_Key_Container()
			{
				if (rsa_csp != null)
				{
					//make key persist in the Csp
					rsa_csp.PersistKeyInCsp = true;
				}
				else
				{
					logger.log_Result(Object_Result.MEMBER_INVALID, "lock_RSA_Key_In_Key_Container: rsa_csp");
				}
			}

			public void clear_RSA()
			{
				try
				{
					//disassociate key in container
					rsa_csp.PersistKeyInCsp = false;

					//release rsa_csp
					rsa_csp.Clear();
				}
				catch (Exception e)
				{
					logger.log_Result(Object_Result.MEMBER_INVALID, "clear_RSA: " + e.Message);
				}
			}

			public void setup_Rijndael_Managed(RijndaelManaged ext_rijndael_managed)
			{
				try
				{
					if (ext_rijndael_managed == null)
					{
						logger.log_Result(Object_Result.ARGUMENT_INVALID, "setup_Rijndael_Managed: ext_rijndael_managed");
						return;
					}

					//Create instance of Rijndael for
					//symmetric encryption of the byte_data
					//using external Rijndael Managed object
					rijndael_managed = new RijndaelManaged();


					rijndael_managed.KeySize = ext_rijndael_managed.KeySize;
					rijndael_managed.BlockSize = ext_rijndael_managed.BlockSize;
					rijndael_managed.Mode = ext_rijndael_managed.Mode;
					rijndael_managed.Padding = ext_rijndael_managed.Padding;
					rijndael_managed.FeedbackSize = ext_rijndael_managed.FeedbackSize;

					//random generate key
					rijndael_managed.GenerateKey();
					//random generate IV
					rijndael_managed.GenerateIV();


				}
				catch (Exception e)
				{
					logger.log_Result(Object_Result.METHOD_MEMBER_FAILED, e.Message);
				}

			}

			public void setup_Key_RSA_Encryption(ref Byte[] ext_key_encrypted, ref Byte[] ext_key)
			{
				try
				{
					if (ext_key==null)
					{

						logger.log_Result(Object_Result.ARGUMENT_INVALID, "setup_Key_RSA_Encryption: ext_key");
						return;
					}

					// Use RSACryptoServiceProvider to
					// encrypt the Rijndael key
					// and store the encrypted key
					// false to use PKCS#1 v1.5 padding
					ext_key_encrypted = rsa_csp.Encrypt(ext_key, false);

				}
				catch (Exception e)
				{
					logger.log_Result(Object_Result.METHOD_MEMBER_FAILED, "setup_Key_RSA_Encryption: " + e.Message);
				}

			}

			public void rijndael_File_Encryption(string abs_in_file, string abs_out_file, Byte[] ext_key_encrypted, ref string ext_message)
			{
				try
				{

					if (abs_in_file == null || abs_in_file == "")
					{
						logger.log_Result(Object_Result.ARGUMENT_INVALID, "rijndael_File_Encryption: abs_in_file");
						return;
					}

					if (abs_out_file == null || abs_out_file == "")
					{
						logger.log_Result(Object_Result.ARGUMENT_INVALID, "rijndael_File_Encryption: abs_out_file");
						return;
					}

					if (ext_key_encrypted==null)
					{
						logger.log_Result(Object_Result.ARGUMENT_INVALID, "rijndael_File_Encryption: ext_key_encrypted");
						return;
					}

					//ext_message test not required as it is to store output of
					//abs_in_file read

					ICryptoTransform encryptor_ct;
					StreamReader input_sr;
					File_Access out_fs;

					//create transform based on Key and IV
					//using Rijndael key for encrypting the byte_data
					//ICryptoTransform^ transform = message_reciever_rjndl->CreateEncryptor(message_reciever_rjndl->Key,message_reciever_rjndl->IV);

					encryptor_ct = rijndael_managed.CreateEncryptor();

					// Create byte arrays to contain
					// the length values of the key and IV.
					Byte[] byte_len_key = new Byte[4];
					Byte[] byte_len_iv = new Byte[4];
					Byte[] byte_len_message = new Byte[4];

					//bits
					int len_key = ext_key_encrypted.Length;
					//bytes
					byte_len_key = BitConverter.GetBytes(len_key);

					//rijndael_managed test carried above
					//bits
					int len_iv = rijndael_managed.IV.Length;
					//bytes
					byte_len_iv = BitConverter.GetBytes(len_iv);

					//get ext_message
					input_sr = new StreamReader(abs_in_file);

					ext_message = input_sr.ReadToEnd();
					input_sr.Close();


					//bits
					int len_message = ext_message.Length;
					//bytes
					byte_len_message = BitConverter.GetBytes(len_message);

					// Write the following to the FileStream
					// for the encrypted file (out_fs):
					out_fs = new File_Access();
					out_fs.create_File(abs_out_file);


					// - length of the key
					out_fs.write_Bytes(byte_len_key, 0, 4);

					// - length of the IV
					out_fs.write_Bytes(byte_len_iv, 0, 4);
					// - encrypted key
					out_fs.write_Bytes(ext_key_encrypted, 0, len_key);
					// - the IV
					out_fs.write_Bytes(rijndael_managed.IV, 0, len_iv);
					// - length of ext_message
					out_fs.write_Bytes(byte_len_message, 0, 4);


					// Now write the cipher text using
					// a CryptoStream for encrypting.
					CryptoStream out_cs = new CryptoStream(out_fs.fs, encryptor_ct, CryptoStreamMode.Write);



					// By encrypting a chunk at
					// a time, you can save memory
					// and accommodate large files.
					int byte_count = 0;
					int offset = 0;

					// block_size_bytes can be any arbitrary size.
					int block_size_bytes = rijndael_managed.BlockSize / 8;

					Byte[] byte_data = new Byte[block_size_bytes];

					File_Access in_fs = new File_Access();

					in_fs.open_File(abs_in_file);

					encryption_progress_meter = 1;


					while ((byte_count = in_fs.read_File_To_Byte_Array(byte_data, 0, block_size_bytes)) > 0)
					{
						offset += byte_count;

						out_cs.Write(byte_data, 0, byte_count);

						encryption_progress_meter = (int)(((float)offset) / in_fs.finfo.Length * 100);

					}

					in_fs.close_File();

					out_cs.Close();

					out_fs.close_File();
				}
				catch (Exception e)
				{
					logger.log_Result(Object_Result.METHOD_MEMBER_FAILED, "rijndael_File_Encryption: " + e.Message);
				}



			}

			public void setup_Key_RSA_Decryption(ref Byte[] key_decrypted, string abs_in_file, ref int len_key)
			{
				try
				{
					if (abs_in_file == null || abs_in_file == "")
					{
						logger.log_Result(Object_Result.ARGUMENT_INVALID, "setup_Key_RSA_Decryption: abs_in_file");
						return;
					}

					// Create byte arrays to get the length of
					// the encrypted key and IV.
					// These values were stored as 4 bytes each
					// at the beginning of the encrypted package.
					Byte[] byte_len_key = new Byte[4];


					// Use FileStream objects to read the encrypted
					// file (in_fs) and save the decrypted file (out_fs).
					File_Access in_fs = new File_Access();
					in_fs.open_File(abs_in_file);

					in_fs.read_File_To_Byte_Array(byte_len_key, 0, 3);

					// Convert the lengths to integer values.
					len_key = BitConverter.ToInt32(byte_len_key, 0);

					// Create the byte arrays for
					// the encrypted Rijndael key,
					// the IV, and the cipher text.
					Byte[] key_encrypted = new Byte[len_key];


					// Extract the key and IV
					// starting from index 8
					// after the length values.
					in_fs.seek(8, SeekOrigin.Begin);
					in_fs.read_File_To_Byte_Array(key_encrypted, 0, len_key);


					in_fs.close_File();


					//store the encrypted key
					//false to use PKCS#1 v1.5 padding
					key_decrypted = rsa_csp.Decrypt(key_encrypted, false);


				}
				catch (Exception e)
				{
					logger.log_Result(Object_Result.METHOD_MEMBER_FAILED, "setup_Key_RSA_Decryption: " + e.Message);
				}

			}

			public void rijndael_File_Decryption(string abs_in_file, string abs_out_file, ref string ext_message, Byte[] key_decrypted, int len_key)
			{
				try
				{

					if (abs_in_file == null || abs_in_file == "")
					{
						logger.log_Result(Object_Result.ARGUMENT_INVALID, "rijndael_File_Decryption: abs_in_file");
						return;
					}

					if (abs_out_file == null || abs_out_file == "")
					{
						logger.log_Result(Object_Result.ARGUMENT_INVALID, "rijndael_File_Decryption: abs_out_file");
						return;
					}

					if (key_decrypted==null)
					{
						logger.log_Result(Object_Result.ARGUMENT_INVALID, "rijndael_File_Decryption: key_decrypted");
						return;
					}

					//input test of len_key skipped as positive value always supplied

					// Create byte arrays to get the length of
					// the encrypted key and IV.
					// These values were stored as 4 bytes each
					// at the beginning of the encrypted package.
					Byte[] byte_len_iv = new Byte[4];
					Byte[] byte_len_message = new Byte[4];


					// Use FileStream objects to read the encrypted
					// file (in_fs) and save the decrypted file (out_fs).
					File_Access in_fs = new File_Access();

					in_fs.open_File(abs_in_file);
					//{


						//in_fs->Read(byte_len_key, 0, 3);
						in_fs.seek(4, SeekOrigin.Begin);
						in_fs.read_File_To_Byte_Array(byte_len_iv, 0, 3);


						// Convert the lengths to integer values.
						int len_iv = BitConverter.ToInt32(byte_len_iv, 0);


						// Create the byte arrays for
						// the encrypted Rijndael key,
						// the IV, and the cipher text.
						Byte[] byte_iv = new Byte[len_iv];

						// Extract the IV
						// starting from index 8
						// after the length values.
						in_fs.seek(8 + len_key, SeekOrigin.Begin);
						in_fs.read_File_To_Byte_Array(byte_iv, 0, len_iv);
						//length of message
						in_fs.seek(8 + len_key + len_iv, SeekOrigin.Begin);
						in_fs.read_File_To_Byte_Array(byte_len_message, 0, 3);

						int len_message = BitConverter.ToInt32(byte_len_message, 0);

						// Determine the start postition of
						// the ciphter text (start_pos)
						// and its length(remaining_len).
						// len_key + Key value
						// len_iv  + IV value
						// len_message
						int start_pos = 4 + len_key + 4 + len_iv + 4;
						int remaining_len = (int)in_fs.fs.Length - start_pos;

						// Use RSACryptoServiceProvider
						// to decrypt the Rijndael key.
						//false to use PKCS#1 v1.5 padding


						// Decrypt the key.
						ICryptoTransform decryptor_ct = rijndael_managed.CreateDecryptor(key_decrypted, byte_iv);


						// Decrypt the cipher text from
						// from the FileSteam of the encrypted
						// file (in_fs) into the FileStream
						// for the decrypted file (out_fs).
						File_Access out_fs = new File_Access();
						out_fs.create_File(abs_out_file);



					//    {

							int byte_count = 0;
							int offset = start_pos;


							// block_size_bytes can be any arbitrary size.
							int block_size_bytes = rijndael_managed.BlockSize / 8;
							Byte[] byte_data = new Byte[block_size_bytes];


							// By decrypting a chunk a time,
							// you can save memory and
							// accommodate large files.

							// Start at the beginning
							// of the cipher text.
							in_fs.seek(start_pos, SeekOrigin.Begin);
							CryptoStream out_cs = new CryptoStream(out_fs.fs, decryptor_ct, CryptoStreamMode.Write);

					//        {

								decryption_progress_meter = 1;

								while ((byte_count = in_fs.read_File_To_Byte_Array(byte_data, 0, block_size_bytes)) > 0)
								{

									offset += byte_count;

									out_cs.Write(byte_data, 0, byte_count);

									decryption_progress_meter = (int)(((float)offset) / in_fs.finfo.Length * 100);

								}

								out_cs.Close();
					//        }
							out_fs.close_File();
					//    }
						in_fs.close_File();
					//}

						StreamReader out_sr = new StreamReader(abs_out_file);

						ext_message = out_sr.ReadToEnd();

						if (len_message > (uint)ext_message.Length)
						{
							ext_message = ext_message.Remove(len_message);
						}

						out_sr.Close();
				}
				catch (Exception e)
				{
					logger.log_Result(Object_Result.METHOD_MEMBER_FAILED, "rijndael_File_Decryption: " + e.Message);
				}

			}



			public int get_Encryption_Progress_Meter()
			{
				return encryption_progress_meter;
			}

			public void reset_Encryption_Progress_Meter()
			{
				encryption_progress_meter = -1;
			}

			public int get_Decryption_Progress_Meter()
			{
				return decryption_progress_meter;
			}

			public void reset_Decryption_Progress_Meter()
			{
				decryption_progress_meter = -1;
			}


			public void setup_SHA1_Managed()
			{
				try
				{
					sha1_man = new SHA1Managed();
				}
				catch (Exception e)
				{
					logger.log_Result(Object_Result.METHOD_MEMBER_FAILED, "setup_SHA1_Managed: " + e.Message);
				}
			}

			public void generate_SHA1_Hash(Byte[] ext_message_bytes, ref Byte[] ext_hash_value)
			{
				try
				{
					if (ext_message_bytes==null)
					{
						logger.log_Result(Object_Result.ARGUMENT_INVALID, "generate_SHA1_Hash: ext_message_bytes");
						return;
					}

					ext_hash_value = sha1_man.ComputeHash(ext_message_bytes);
				}
				catch (Exception e)
				{
					logger.log_Result(Object_Result.METHOD_MEMBER_FAILED, "generate_SHA1_Hash: " + e.Message);
				}

			}

			public void setup_RSAPKCS1SignatureFormatter()
			{
				try
				{
					rsa_sf = new RSAPKCS1SignatureFormatter(rsa_csp);
				}
				catch (Exception e)
				{
					logger.log_Result(Object_Result.METHOD_MEMBER_FAILED, "setup_RSAPKCS1SignatureFormatter: " + e.Message);
				}
			}

			public void setup_Hash_Algorithm_RSAPKCS1SignatureFormatter(string hash_algorithm)
			{
				try
				{
					if (hash_algorithm == null || hash_algorithm == "")
					{
						logger.log_Result(Object_Result.ARGUMENT_INVALID, "setup_Hash_Algorithm_RSAPKCS1SignatureFormatter: hash_algorithm");
						return;
					}

					rsa_sf.SetHashAlgorithm(hash_algorithm);
				}
				catch (Exception e)
				{
					logger.log_Result(Object_Result.METHOD_MEMBER_FAILED, "setup_Hash_Algorithm_RSAPKCS1SignatureFormatter: " + e.Message);
				}
			}

			public void sign_Hash(Byte[] hash_value, ref Byte[] ext_signed_hash)
			{
				try
				{
					if (hash_value==null)
					{
						logger.log_Result(Object_Result.ARGUMENT_INVALID, "sign_Hash: hash_value");
						return;
					}
					ext_signed_hash = rsa_sf.CreateSignature(hash_value);
				}
				catch (Exception e)
				{
					logger.log_Result(Object_Result.METHOD_MEMBER_FAILED, "sign_Hash: " + e.Message);
				}
			}

			public void setup_RSAPKCS1SignatureDeFormatter()
			{
				try
				{
					rsa_sdf = new RSAPKCS1SignatureDeformatter(rsa_csp);
				}
				catch (Exception e)
				{
					logger.log_Result(Object_Result.METHOD_MEMBER_FAILED, "setup_RSAPKCS1SignatureDeFormatter: " + e.Message);
				}
			}

			public void setup_Hash_Algorithm_RSAPKCS1SignatureDeFormatter(string hash_algorithm)
			{
				try
				{
					if (hash_algorithm == null || hash_algorithm == "")
					{
						logger.log_Result(Object_Result.ARGUMENT_INVALID, "setup_Hash_Algorithm_RSAPKCS1SignatureDeFormatter: hash_algorithm");
						return;
					}
					rsa_sdf.SetHashAlgorithm(hash_algorithm);
				}
				catch (Exception e)
				{
					logger.log_Result(Object_Result.METHOD_MEMBER_FAILED, "setup_Hash_Algorithm_RSAPKCS1SignatureDeFormatter: " + e.Message);
				}
			}

			public bool verify_Signed_Hash(Byte[] hash_value, Byte[] signed_hash)
			{
				try
				{
					if (hash_value==null)
					{
						logger.log_Result(Object_Result.ARGUMENT_INVALID, "verify_Signed_Hash: hash_value");
						return false;
					}
					if (signed_hash==null)
					{
						logger.log_Result(Object_Result.ARGUMENT_INVALID, "verify_Signed_Hash: signed_hash");
						return false;
					}

					return rsa_sdf.VerifySignature(hash_value,signed_hash);
				}
				catch (Exception e)
				{
					logger.log_Result(Object_Result.METHOD_MEMBER_FAILED, "verify_Signed_Hash: " + e.Message);
					return false;
				}
			}







	}

}