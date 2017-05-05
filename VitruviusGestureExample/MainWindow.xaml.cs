using System.Windows;
using Microsoft.Kinect;
using LightBuzz.Vitruvius;

namespace VitruviusGestureExample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// All credits goes to Vitruvius for their framework.
    /// This tutorial is created by Toh Bing Cheng, the pdf file can be found here: 
    public partial class MainWindow : Window
    {
        //initializing the components we need for this example.
        KinectSensor _sensor;
        MultiSourceFrameReader _reader;
        GestureController _gestureController;
        public MainWindow()
        {
            InitializeComponent();

            //Gets the default sensor.
            _sensor = KinectSensor.GetDefault();

            if (_sensor != null)
            {
                // open the kinect sensor
                _sensor.Open();

                //since we only state the FrameSourceTypes as body, only the body frames would be recieved, you can add colour or infrared too!
                _reader = _sensor.OpenMultiSourceFrameReader(FrameSourceTypes.Body);
                //Reader_MultiSourceFrameArrived would be executed whenever a frame is arrived
                _reader.MultiSourceFrameArrived += Reader_MultiSourceFrameArrived;

                _gestureController = new GestureController();
                //trigger the method GestureController_GestureRecognized once a gesture is being recognized
                _gestureController.GestureRecognized += GestureController_GestureRecognized;
            }
        }

        void Reader_MultiSourceFrameArrived(object sender, MultiSourceFrameArrivedEventArgs e)
        {
            //get the frame from the kinect
            var reference = e.FrameReference.AcquireFrame();

            // Body
            using (var frame = reference.BodyFrameReference.AcquireFrame())
            {
                if (frame != null)
                {
                    //detect the nearest body to the kinect
                    Body body = frame.Bodies().Closest();

                    if (body != null)
                    {
                        //update the skeleton of the body
                        _gestureController.Update(body);
                    }
                }
            }
        }

        void GestureController_GestureRecognized(object sender, GestureEventArgs e)
        {
            //This would be the different types of gesture found inside the enum
            var gesture = e.GestureType;

            switch (gesture)
            {
                //you can choose the event you want for the detected gestures
                case (GestureType.JoinedHands): break;
                case (GestureType.Menu): break;
                case (GestureType.SwipeDown):
                    lbDisplay.Content = gesture.ToString();
                    break;
                case (GestureType.SwipeLeft):
                    lbDisplay.Content = gesture.ToString();
                    break;
                case (GestureType.SwipeRight):
                    lbDisplay.Content = gesture.ToString();
                    break;
                case (GestureType.SwipeUp):
                    lbDisplay.Content = gesture.ToString();
                    break;
                case (GestureType.WaveLeft): break;
                case (GestureType.WaveRight): break;
                case (GestureType.ZoomIn): break;
                case (GestureType.ZoomOut): break;
            }

        }




    }
}
