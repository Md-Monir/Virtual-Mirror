using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Kinect;
using System.IO;
using Microsoft.Samples.Kinect.SwipeGestureRecognizer;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace FinalKinectApplicationV1
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private const int maxDis = 4400;
        private const int minDis = 4000;
        private Boolean onceChecked = false;
        private int globalCount = 0;
        private int writeCount = 0;
        private int maxWriteCount = 150;
        int countToBeCompared = 0;
        private int finalAvgShoulderDiffX = 0;
        private int totalCountNumber = 0;
        double shoulderDistanceX = 0;
        private int shirtCount = 1;

        private int smallSizelimit = 590;
        private int mediumSizelimit = 630;
        private int largeSizelimit = 00;

        private readonly Recognizer activeRecognizer;
        private readonly string[] picturePathsForLarge = CreatePicturePathsForLarge();
        private readonly string[] picturePathsForSmall = CreatePicturePathsForSmall();
        private readonly string[] picturePathsForMedium = CreatePicturePathsForMedium();
        private readonly string[] picturePathsForNull = CreatePicturePathsForNull();
        


        private static readonly JointType[][] SkeletonSegmentRuns = new JointType[][]
        {
            new JointType[]
            { 
                JointType.Head, JointType.ShoulderCenter, JointType.HipCenter 
            },
            new JointType[]
            { 
                JointType.HandLeft, JointType.WristLeft, JointType.ElbowLeft, JointType.ShoulderLeft,
                JointType.ShoulderCenter,
                JointType.ShoulderRight, JointType.ElbowRight, JointType.WristRight, JointType.HandRight
            },
            new JointType[]
            {
                JointType.FootLeft, JointType.AnkleLeft, JointType.KneeLeft, JointType.HipLeft,
                JointType.HipCenter,
                JointType.HipRight, JointType.KneeRight, JointType.AnkleRight, JointType.FootRight
            }
        };

        private KinectSensor nui;


        byte[] colorBytes;

        private bool isDisconnectedField = true;

        private string disconnectedReasonField;

        private Skeleton[] skeletons = new Skeleton[0];

        private DateTime highlightTime = DateTime.MinValue;

        private int highlightId = -1;

        private int nearestId = -1;

        private int indexField = 1;

        public MainWindow()
        {

            InitializeComponent();

            this.activeRecognizer = this.CreateRecognizer();

            Loaded += this.OnMainWindowLoaded;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsDisconnected
        {
            get
            {
                return this.isDisconnectedField;
            }

            private set
            {
                if (this.isDisconnectedField != value)
                {
                    this.isDisconnectedField = value;

                    if (this.PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("IsDisconnected"));
                    }
                }
            }
        }

        public string DisconnectedReason
        {
            get
            {
                return this.disconnectedReasonField;
            }
            private set
            {
                if (this.disconnectedReasonField != value)
                {
                    this.disconnectedReasonField = value;

                    if (this.PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("DisconnectedReason"));
                    }
                }
            }
        }

        public int Index
        {
            get
            {
                
                return this.indexField;

            }

            set
            {
                if (this.indexField != value)
                {
                    this.indexField = value;

                    if (this.PropertyChanged == null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Index"));
                    }
                }
            }
        }

        public BitmapImage PreviousPicture { get; private set; }

        public BitmapImage Picture { get; private set; }

        public BitmapImage NextPicture { get; private set; }

        

        private static string[] CreatePicturePathsForLarge()
        {
            var list = new List<string>();

            var commonPicturesPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonPictures);
            //list.AddRange(Directory.GetFiles(commonPicturesPath, "*.jpg", SearchOption.AllDirectories));
            //if (list.Count == 0)
            //{
            //    list.AddRange(Directory.GetFiles(commonPicturesPath, "*.png", SearchOption.AllDirectories));
            //}

            if (list.Count == 0)
            {
                var myPicturesPath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                list.AddRange(Directory.GetFiles(myPicturesPath, "*.jpg", SearchOption.AllDirectories));
                if (list.Count == 0)
                {
                    list.AddRange(Directory.GetFiles(myPicturesPath, "*.png", SearchOption.AllDirectories));
                }
            }

            return list.ToArray();
        }

        private static string[] CreatePicturePathsForSmall()
        {
            var list = new List<string>();

            var commonPicturesPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonPictures);

            if (list.Count == 0)
            {
                var myPicturesPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                list.AddRange(Directory.GetFiles(myPicturesPath, "*.jpg", SearchOption.TopDirectoryOnly));
                if (list.Count == 0)
                {
                    list.AddRange(Directory.GetFiles(myPicturesPath, "*.png", SearchOption.TopDirectoryOnly));
                }
            }

            return list.ToArray();
        }

        private static string[] CreatePicturePathsForMedium()
        {
            var list = new List<string>();

            var commonPicturesPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonPictures);

            if (list.Count == 0)
            {
                var myPicturesPath = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
                list.AddRange(Directory.GetFiles(myPicturesPath, "*.jpg", SearchOption.TopDirectoryOnly));
                if (list.Count == 0)
                {
                    list.AddRange(Directory.GetFiles(myPicturesPath, "*.png", SearchOption.TopDirectoryOnly));
                }
            }

            return list.ToArray();
        }
        private static string[] CreatePicturePathsForNull()
        {
            var list = new List<string>();

            var commonPicturesPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonPictures);

            if (list.Count == 0)
            {
                var myPicturesPath = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);
                list.AddRange(Directory.GetFiles(myPicturesPath, "*.jpg", SearchOption.TopDirectoryOnly));
                if (list.Count == 0)
                {
                    list.AddRange(Directory.GetFiles(myPicturesPath, "*.png", SearchOption.TopDirectoryOnly));
                }
            }

            return list.ToArray();
        }

        private BitmapImage LoadPicture(int index)
        {
            BitmapImage value = null;

            
            if (finalAvgShoulderDiffX <= smallSizelimit && finalAvgShoulderDiffX > 0)
            {
                value = InitiaLizeImagePathToValueArray(picturePathsForSmall, index);
                ShirtDetailsTextBlock.Text = "Dress Details: " + "\n" + " Available color : Red"+" Black" +" Blue";
            }
            else if (finalAvgShoulderDiffX >smallSizelimit && finalAvgShoulderDiffX <= mediumSizelimit)
            {
                value = InitiaLizeImagePathToValueArray(picturePathsForMedium, index);
                ShirtDetailsTextBlock.Text = "Dress Details: " + "\n" + " Available color : Red" + " Black";

            }
            else if (finalAvgShoulderDiffX > mediumSizelimit)
            {

                value = InitiaLizeImagePathToValueArray(picturePathsForLarge, index);
                ShirtDetailsTextBlock.Text = "Dress Details: " + "\n" + " Available color : Red" +" Black" +" Blue";

            }

            else if (finalAvgShoulderDiffX == 0)
            {

                BitmapImage tmpImage = new BitmapImage();
                tmpImage.BeginInit();
                string filename = "google.png";
                tmpImage.UriSource = new Uri(filename, UriKind.Relative);
                tmpImage.EndInit();
                value = tmpImage;
            }

            return value;
        }

        private BitmapImage InitiaLizeImagePathToValueArray(string[] strings, int tmpIndex)
        {
            BitmapImage tmpValue = null;

            if (strings.Length != 0)
            {
                var actualIndex = tmpIndex % strings.Length;
                //...............................

                //.............................
                if (actualIndex < 0)
                {
                    actualIndex += strings.Length;
                }

                Debug.Assert(0 <= actualIndex, "Index used will be non-negative");
                Debug.Assert(actualIndex < strings.Length, "Index is within bounds of path array");

                try
                {
                    tmpValue = new BitmapImage(new Uri(strings[actualIndex]));
                }
                catch (NotSupportedException)
                {
                    tmpValue = null;
                }
            }
            else
            {
                tmpValue = null;
            }
            return tmpValue;
        }

        private int ActualShirtNumber(int actualIndex)
        {
            int c = 0;

            if (actualIndex == 0)
            {
                c = 1;
            }
            else if (actualIndex == 1)
            {
                c = 2;
            }
            else if (actualIndex == 2)
            {
                c = 3;
            }

            return c;
        }

        private Recognizer CreateRecognizer()
        {

            var recognizer = new Recognizer();

            recognizer.SwipeRightDetected += (s, e) =>
            {
                if (e.Skeleton.TrackingId == nearestId)
                {
                    Index++;


                    this.PreviousPicture = this.Picture;
                    this.Picture = this.NextPicture;
                    this.NextPicture = LoadPicture(Index + 1);




                    if (this.PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("PreviousPicture"));
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Picture"));
                        this.PropertyChanged(this, new PropertyChangedEventArgs("NextPicture"));
                    }

                    var storyboard = Resources["LeftAnimate"] as Storyboard;
                    if (storyboard != null)
                    {
                        storyboard.Begin();
                    }

                    HighlightSkeleton(e.Skeleton);
                }
            };


            recognizer.SwipeLeftDetected += (s, e) =>
            {
                if (e.Skeleton.TrackingId == nearestId)
                {
                    Index--;


                    this.NextPicture = this.Picture;
                    this.Picture = this.PreviousPicture;
                    this.PreviousPicture = LoadPicture(Index - 1);



                    if (this.PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("PreviousPicture"));
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Picture"));
                        this.PropertyChanged(this, new PropertyChangedEventArgs("NextPicture"));
                    }

                    var storyboard = Resources["RightAnimate"] as Storyboard;
                    if (storyboard != null)
                    {
                        storyboard.Begin();
                    }

                    HighlightSkeleton(e.Skeleton);
                }
            };

            return recognizer;
        }

        private void InitializeNui()
        {
            this.UninitializeNui();

            var index = 0;
            while (this.nui == null && index < KinectSensor.KinectSensors.Count)
            {
                try
                {
                    this.nui = KinectSensor.KinectSensors[index];

                    this.nui.Start();

                    this.IsDisconnected = false;
                    this.DisconnectedReason = null;
                }
                catch (IOException ex)
                {
                    this.nui = null;

                    this.DisconnectedReason = ex.Message;
                }
                catch (InvalidOperationException ex)
                {
                    this.nui = null;

                    this.DisconnectedReason = ex.Message;
                }

                index++;
            }

            if (this.nui != null)
            {
                this.nui.SkeletonStream.Enable();


                //.............................................
                this.nui.ColorStream.Enable();

                nui.ColorFrameReady += new EventHandler<ColorImageFrameReadyEventArgs>(SensorColorFrameReady);
                //.............................................


                this.nui.SkeletonFrameReady += this.OnSkeletonFrameReady;
            }
        }

        private void UninitializeNui()
        {
            if (this.nui != null)
            {
                this.nui.SkeletonFrameReady -= this.OnSkeletonFrameReady;

                this.nui.Stop();

                this.nui = null;
            }

            this.IsDisconnected = true;
            this.DisconnectedReason = null;
        }



        private void OnMainWindowLoaded(object sender, RoutedEventArgs e)
        {

            this.InitializeNui();


            KinectSensor.KinectSensors.StatusChanged += (s, ee) =>
            {
                switch (ee.Status)
                {
                    case KinectStatus.Connected:
                        if (nui == null)
                        {
                            Debug.WriteLine("New Kinect connected");

                            InitializeNui();
                        }
                        else
                        {
                            Debug.WriteLine("Existing Kinect signalled connection");
                        }

                        break;
                    default:
                        if (ee.Sensor == nui)
                        {
                            Debug.WriteLine("Existing Kinect disconnected");


                        }
                        else
                        {
                            Debug.WriteLine("Other Kinect event occurred");
                        }

                        break;
                }
            };
        }

        private void OnSkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {

            using (var frame = e.OpenSkeletonFrame())
            {
                if (frame != null)
                {

                    if (this.skeletons.Length != frame.SkeletonArrayLength)
                    {
                        this.skeletons = new Skeleton[frame.SkeletonArrayLength];
                    }

                    frame.CopySkeletonDataTo(this.skeletons);
                    var newNearestId = -1;
                    var nearestDistance2 = double.MaxValue;

                    foreach (var skeleton in this.skeletons)
                    {

                        if (skeleton.TrackingState == SkeletonTrackingState.Tracked)
                        {
                            var distance2 = (skeleton.Position.X * skeleton.Position.X) +
                                (skeleton.Position.Y * skeleton.Position.Y) +
                                (skeleton.Position.Z * skeleton.Position.Z);

                            if (distance2 < nearestDistance2)
                            {
                                newNearestId = skeleton.TrackingId;
                                nearestDistance2 = distance2;
                            }
                        }
                    }

                    if (this.nearestId != newNearestId)
                    {
                        this.nearestId = newNearestId;
                    }

                    this.activeRecognizer.Recognize(sender, frame, this.skeletons);


                    this.DrawStickMen(this.skeletons);
                    Skeleton closestSkeleton = (from s in skeletons
                                                where s.TrackingState == SkeletonTrackingState.Tracked &&
                                                      s.Joints[JointType.Head].TrackingState == JointTrackingState.Tracked
                                                select s).OrderBy(s => s.Joints[JointType.Head].Position.Z)
                                                    .FirstOrDefault();
                    if (closestSkeleton == null)
                        return;

                    var righShoulder = closestSkeleton.Joints[JointType.ShoulderRight];
                    var leftShoulder = closestSkeleton.Joints[JointType.ShoulderLeft];
                    var centerShoulder = closestSkeleton.Joints[JointType.ShoulderCenter];
                    ProcessShoulder(righShoulder, leftShoulder, centerShoulder);


                }
            }
        }

        private void ProcessShoulder(Joint righShoulder, Joint leftShoulder, Joint centerShoulder)
        {
            double rightZ = (righShoulder.Position.Z) * 1800;
            double leftZ = (leftShoulder.Position.Z) * 1800;
            double middleZ = (centerShoulder.Position.Z) * 1800;


            if (middleZ <= maxDis && middleZ >= minDis)
            {
                TrackingLabel.Content = "Tracked !!!";

                

                if (writeCount <= 151)
                {
                    CalculateShoulderDifference(righShoulder, rightZ, leftShoulder, leftZ, centerShoulder, middleZ);
                }


                SetDress(centerShoulder);

            }
            else
            {
                current.Visibility = Visibility.Hidden;
                TrackingLabel.Content = "Not !!!";
                ShoulderTrackingLabel.Text = "";
            }
        }

        private void SetDress(Joint spine)
        {
            var point = nui.MapSkeletonPointToColor(spine.Position, nui.ColorStream.Format);
            current.Visibility = Visibility.Visible;
            this.Picture = this.LoadPicture(this.Index);
            current.Source = Picture;
            
            Canvas.SetLeft(current, point.X - (current.ActualWidth - 65));
            Canvas.SetTop(current, point.Y - 16);
        }


        private void CalculateShoulderDifference(Joint righShoulder, double rightZ, Joint leftShoulder, double leftZ, Joint centerShoulder, double middleZ)
        {
            int n = 60;
            int rightMin = (int)(middleZ - n);
            int rightMax = (int)(middleZ + n);
            int leftMin = (int)(middleZ - n);
            int leftMax = (int)(middleZ + n);

            string msg2 = "";

            if ((rightZ <= rightMin) || (rightZ >= rightMax) || (leftZ <= leftMin) || (leftZ >= leftMax))
            {

                if (rightZ <= rightMin)
                {
                    msg2 += "Right Shoulder Back\n";
                } if (rightZ >= rightMax)
                {
                    msg2 += "Right Shoulder front\n";
                }
                if (leftZ <= leftMin)
                {
                    msg2 += "Left Shoulder Back\n";
                }
                if (leftZ >= leftMax)
                {
                    msg2 += "Left Shoulder front\n";
                }
                ShoulderTrackingLabel.Text = msg2;
            }
            else
            {

                ShoulderTrackingLabel.Text = "Please Stand Still...";

                if (writeCount <= 150)
                {
                    if (writeCount == countToBeCompared)
                    {
                        shoulderDistanceX += (Math.Abs(righShoulder.Position.X - leftShoulder.Position.X) * 1800);

                        countToBeCompared += 1;
                        totalCountNumber++;
                        Debug.WriteLine(writeCount + "\t" + shoulderDistanceX);
                    }
                    writeCount++;
                }
                else
                {
                    if (writeCount == 151)
                    {
                        finalAvgShoulderDiffX = (int)(shoulderDistanceX / totalCountNumber);

                        if (finalAvgShoulderDiffX < smallSizelimit)
                        {
                            shirtSizelabel.Content = "SMALL !!!\n" + (finalAvgShoulderDiffX);
                        }
                        else if (finalAvgShoulderDiffX < mediumSizelimit)
                        {
                            shirtSizelabel.Content = "MEDIUM !!!\n" + (finalAvgShoulderDiffX);
                        }
                        else if (finalAvgShoulderDiffX >= mediumSizelimit)
                        {
                            shirtSizelabel.Content = "LARGE !!!\n" + (finalAvgShoulderDiffX);
                        }

                        this.Picture = this.LoadPicture(this.Index);


                        ShoulderTrackingLabel.Visibility = Visibility.Hidden;
                        Debug.WriteLine(writeCount + "\t" + finalAvgShoulderDiffX);
                    }
                }


            }
        }

        private void HighlightSkeleton(Skeleton skeleton)
        {
            this.highlightTime = DateTime.UtcNow + TimeSpan.FromSeconds(0.5);

            this.highlightId = skeleton.TrackingId;
        }


        private void DrawStickMen(Skeleton[] skeletons)
        {
            StickMen.Children.Clear();

            foreach (var skeleton in skeletons)
            {
                if (skeleton.TrackingState == SkeletonTrackingState.Tracked)
                {
                    this.DrawStickMan(skeleton, Brushes.WhiteSmoke, 7);
                }
            }

            foreach (var skeleton in skeletons)
            {
                if (skeleton.TrackingState == SkeletonTrackingState.Tracked)
                {
                    var brush = DateTime.UtcNow < this.highlightTime && skeleton.TrackingId == this.highlightId ? Brushes.Red :
                        skeleton.TrackingId == this.nearestId ? Brushes.Black : Brushes.Gray;

                    this.DrawStickMan(skeleton, brush, 3);
                }
            }
        }


        private void DrawStickMan(Skeleton skeleton, Brush brush, int thickness)
        {
            Debug.Assert(skeleton.TrackingState == SkeletonTrackingState.Tracked, "The skeleton is being tracked.");

            foreach (var run in SkeletonSegmentRuns)
            {
                var next = this.GetJointPoint(skeleton, run[0]);
                for (var i = 1; i < run.Length; i++)
                {
                    var prev = next;
                    next = this.GetJointPoint(skeleton, run[i]);

                    var line = new Line
                    {
                        Stroke = brush,
                        StrokeThickness = thickness,
                        X1 = prev.X,
                        Y1 = prev.Y,
                        X2 = next.X,
                        Y2 = next.Y,
                        StrokeEndLineCap = PenLineCap.Round,
                        StrokeStartLineCap = PenLineCap.Round
                    };

                    StickMen.Children.Add(line);
                }
            }
        }


        private Point GetJointPoint(Skeleton skeleton, JointType jointType)
        {
            var joint = skeleton.Joints[jointType];

            var point = new Point
            {
                X = (StickMen.Width / 2) + (StickMen.Height * joint.Position.X / 3),
                Y = (StickMen.Width / 2) - (StickMen.Height * joint.Position.Y / 3)
            };

            return point;
        }


        private void SensorColorFrameReady(object sender, ColorImageFrameReadyEventArgs e)
        {
            using (var image = e.OpenColorImageFrame())
            {
                if (image == null)
                    return;

                if (colorBytes == null || colorBytes.Length != image.PixelDataLength)
                {
                    colorBytes = new byte[image.PixelDataLength];
                }

                image.CopyPixelDataTo(colorBytes);


                int length = colorBytes.Length;
                for (int i = 0; i < length; i += 4)
                {
                    colorBytes[i + 3] = 255;
                }

                BitmapSource source = BitmapSource.Create(image.Width,
                    image.Height,
                    96,
                    96,
                    PixelFormats.Bgra32,
                    null,
                    colorBytes,
                    image.Width * image.BytesPerPixel);
                VideoImage.Source = source;
            }
        }


    }
}