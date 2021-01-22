using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Controls_1_0
{
    class Analog_Clock: PictureBox
    {
        private Timer ClockTimer = new Timer();

        private Pen second_pen = new Pen(Color.Green, 1.0f);
        private Pen minute_pen = new Pen(Color.Blue, 2.0f);
        private Pen hour_pen = new Pen(Color.Red, 3.0f);
        private Pen major_seg_pen = new Pen(Color.Black, 3.0f);
        private Pen minor_seg_pen = new Pen(Color.Black, 3.0f);
        private Pen dial_pen = new Pen(Color.AliceBlue, 2.0f);
        private Brush dial_brush = new SolidBrush(Color.White);

        private Color back_color = DefaultBackColor;

        public bool draw_primitive = true;

        /// Indicates if the minor segements are shown
        public bool show_minor_segments = true;

        /// Indicates if the major segements are shown
        public bool show_major_segments = true;

        // Indicates if the second hand is shown
        private bool show_second_hand = true;

        // Indicates if the minute hand is shown
        private bool show_minute_hand = true;

        // Indicates if the hour hand is shown
        private bool show_hour_hand = true;
    
        public Analog_Clock():base()
        {
            //Set the double buffered to true to reduce flickering of the graphics
            DoubleBuffered = true;

            //Create the timer and start it
            ClockTimer.Tick += ClockTimer_Tick;
            ClockTimer.Interval = 1;
            ClockTimer.Enabled = true;
            ClockTimer.Start();

        }

        public void set_Dial_Color(Color ext_color)
        {
            dial_brush = new SolidBrush(ext_color);
        }

        public void set_Clock_Background_Color(Color ext_bg_color)
        {
            back_color = ext_bg_color;
        }

        public void set_Dial_Pen(Pen ext_pen)
        {
            dial_pen = ext_pen;
        }

        public void set_Major_Segment_Pen(Pen ext_pen)
        {
            major_seg_pen = ext_pen;
        }

        public void set_Minor_Segment_Pen(Pen ext_pen)
        {
            minor_seg_pen = ext_pen;
        }

        public void set_Second_Hand_Pen(Pen ext_pen)
        {
            second_pen = ext_pen;
        }

        public void set_Minute_Hand_Pen(Pen ext_pen)
        {
            minute_pen = ext_pen;
        }

        public void set_Hour_Hand_Pen(Pen ext_pen)
        {
            hour_pen = ext_pen;
        }

        /// The tick event for the timer
        private void ClockTimer_Tick(object sender, EventArgs e)
        {
            Refresh();
        }

        private void draw_Primitive_Clock(PaintEventArgs pe)
        {
            //Clear the graphics to the back color of the control
            pe.Graphics.Clear(back_color);

            //fill dial
            pe.Graphics.FillEllipse(dial_brush, 0 + dial_pen.Width, 0 + dial_pen.Width, Size.Width - 1 - dial_pen.Width * 2, Size.Height - 1 - dial_pen.Width * 2);

            //Draw the border of the dial
            pe.Graphics.DrawEllipse(dial_pen, 0+dial_pen.Width, 0+dial_pen.Width, Size.Width - 1 - dial_pen.Width*2 , Size.Height - 1 - dial_pen.Width*2);

            

            //Find the radius of the control by dividing the width by 2
            float radius = (Size.Width / 2) - dial_pen.Width*2;

            //Find the origin of the circle by dividing the width and height of the control
            PointF origin = new PointF(Size.Width / 2, Size.Height / 2);

            //Draw only if ShowMinorSegments is true
            if (show_minor_segments)
            {
                //Draw the minor segments for the control
                for (float i = 0f; i != 366f; i += 6f)
                {
                    pe.Graphics.DrawLine(minor_seg_pen, PointOnCircle(radius, i, origin), PointOnCircle(radius - 10, i, origin));
                }
            }

            //Draw only if ShowMajorSegments is true;
            if (show_major_segments)
            {
                //Draw the Major segments for the clock
                for (float i = 0f; i != 390f; i += 30f)
                {
                    pe.Graphics.DrawLine(major_seg_pen, PointOnCircle(radius - 1, i, origin), PointOnCircle(radius - 21, i, origin));
                }
            }

            //Draw only if ShowSecondHand is true
            if (show_second_hand)
            {
                //Draw the second hand
                pe.Graphics.DrawLine(second_pen, origin, PointOnCircle(radius, DateTime.Now.Second * 360f/60f, origin));
            }

            //Draw only if ShowMinuteHand is true
            if (show_minute_hand)
            {
                //Draw the minute hand
                pe.Graphics.DrawLine(minute_pen, origin, PointOnCircle(radius * 0.75f, DateTime.Now.Minute * 360f/60f, origin));
            }

            //Draw only if ShowHourHand is true
            if (show_hour_hand)
            {
                //Draw the hour hand
                pe.Graphics.DrawLine(hour_pen, origin, PointOnCircle(radius * 0.50f, DateTime.Now.Hour  *  (360f/12f) + (DateTime.Now.Minute/15*7), origin));
            }
        }

        private void draw_Graphic_Clock(PaintEventArgs pe)
        {
            //background is a texture
            //all shapes are textured
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            try
            {
                base.OnPaint(pe);
                
                if (draw_primitive)
                {
                    draw_Primitive_Clock(pe);
                }
                else
                    draw_Graphic_Clock(pe);
                
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            //Make sure the control is square
            if (Size.Height != Size.Width)
                Size = new Size(Size.Width, Size.Width);

            //Redraw the control
            Refresh();
        }

        // Find the point on the circumference of a circle
        private PointF PointOnCircle(float radius, float angleInDegrees, PointF origin)
        {
            //Find the x and y using the parametric equation for a circle
            float x = (float)(radius * Math.Cos((angleInDegrees - 90f) * Math.PI / 180F)) + origin.X;
            float y = (float)(radius * Math.Sin((angleInDegrees - 90f) * Math.PI / 180F)) + origin.Y;

            /*Note : The "- 90f" is only for the proper rotation of the clock.
             * It is not part of the parament equation for a circle*/

            //Return the point
            return new PointF(x, y);
        }

        
        
    }
}


    

