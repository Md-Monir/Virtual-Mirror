# Virtual-Mirror
Abstract

In our day to day life shopping kills a large amount of time. When we think about shopping at first huge crowd and trial room problems came in our mind. So, to enhance users shopping experience and to spend less time on maintain queue for fitting room, our goal was to presents a virtual mirror. This allows a person to check how a dress looks like and which color is suitable on a person’s body.  Moreover, it shows dress details and users body measurement when users try on those virtual clothes. We used Microsoft Kinect sensors to track user skeleton movement. The cloths are simulated in such a way that will represent an environment for the user like the mirror. We designed our system in such a way that we can get human body measurement from skeleton joint points and based on that measurement values we apply dynamic dresses on human body. At first we emphasize to develop fixed position, dynamic dress, hand gesture and body measurement. In future, we have a plan to make it as internet based application and 3D modeling virtual mirror.

1.0   INTRODUCTION

In this modern era of revolution everyone is depending more and more on technology. According to this flow of development in technology the common definition of shopping is also changing by time to time. Now there comes online shopping which got popularity. Now-a-days online shopping or shopping through web is getting more popular because it is saving huge amount of valuable time of the shoppers and also reducing other hassles. Moreover, online shopping is being accepted widely all over the world. More than 85% of world’s population has ordered goods over the internet during the recent years [1]. People are getting more attracted to the online shopping because of its extra features or offers like free home delivery, cash on delivery, and different kinds of discounts. However, it has a significant drawback- this method is not being accepted by all peoples as there is no surety that the delivered goods or cloths will be according to the expectation of the customer. Although customers can find all the description of the cloth like style, size, color fabric and other features through the web page, but they cannot determine whether the cloth is exactly suitable for their own style, color, size and other aspects. Therefore, the delivered clothes might also not fit the customers [2]. Previously, a number of researchers worked on this area to overcome the problems of online shopping. The researchers came up with an idea of virtually try the dresses or clothes so that the user do not have to try it physically [3, 4]. So, to enhance users shopping experience and to spend less time on maintain queue for fitting room, our goal is to presents a virtual mirror model using gesture recognition technique. This allows a person to check how a dress looks like and which color is suitable on a person’s body. Moreover, it shows user's body measurement when users try on virtual clothes. In the proposed model, we used Microsoft Kinect sensors to track user skeleton movement and depth image. The cloths are simulated in such a way that will represent an environment for the user like the mirror. In addition, we developed an algorithm for matching up all the motions between the virtual cloths and the human body.
We have read some research papers. Among them we have chosen “Magic Mirror Using Kinect,” by A. B. Habib, A. Asad, W.B. Omar, BRAC University (2015). By studying this research paper, we found the following limitations:
	User need to move to adjust the cloth within his or her body.
	Dresses were not accurate to the body shape. 
	Used no user-interface.
	Used fixed number of static dress 
To overcome the limitations, we proposed a concept of real time virtual dressing room [3]. As mirrors are indispensable objects in our lives, the capability of simulating a mirror on a computer display or screen, augmented with virtual scenes and objects, opens the door for solving the major drawback in online shopping concept. An interactive Mirror could enable the shoppers to virtually try clothes, dresses using gesture-based interaction [5]. Therefore, we have proposed a method having the following features. 
	Use a static position.
	Display size of dresses. 
	Use hand gesture and improved user interface.
	Dynamic dresses based on user size
In this thesis, gesture based interaction techniques are used in order to create a virtual mirror for the virtualization of various clothes. When trying a new cloth the shoppers look into the shop’s mirror in a shop. We create the same impression but for virtual clothes that the customer can choose independently. For that purpose, we replace the real mirror by a display that shows the mirrored input of a camera capturing the body skeleton of a person.


2.0 SYSTEM DESIGN

2.1   System Implementation

Figure 1 shows a detailed block diagram of system implementation of our proposed model.  

 
Figure 1. System implementation flow


2.1.1 User and Kinect

Human body act as input source and by Kinect we take that input. Both Kinect and raw data from user are taken parallel. After that all information passes to the next session as input.

2.1.2 Position and Screen Setup

After taking raw values from user and Kinect the system measures the position of user and set screen setup.

2.1.3 Screen

Screen will process the raw data and change it some frame model and will show into the screen.

2.1.4 Position Comparison

In this stage our code will compare each array with previous array of frame to identify the rotation of human.

2.1.5 Input

At this stage system will save all the raw data for further calculation.

2.1.7 Computation

The raw data are compiled and also compute the dress model data.

2.1.8 Compare and Coordinate

From previous step’s raw values, it will compare human model values with dress model values take some point to coordinate them.

2.1.9 Display

After doing all this process our system will display the final output. Then it will prepare itself for next input.

2.2   Application Flow

Figure 2 shows the application flow of the proposed model. 
 
Figure 2. Application Flow of the system

Our application flow starts from Kinect Controller. It works as a main class which also controls other sub class like measurement, cloth collider. There is also a user interface, a Menu Controller which is derived from Mono-behavior.

2.2   Process Flow

Figure 3 show the process flow of the system.

 
Figure 3. Process flow


3.0 SYSTEM IMPLEMENTATION

3.1   Color Stream

Color Stream is mainly used to display the view of the camera from the Kinect to the display or screen. Kinect provides color image data in different formats and resolutions. The color stream is displayed in an image on WPF which is continuously updated by each color stream packet.
   The format defines whether the data stream in encoded as RGB, YUV, and Bayer. Resolution is about how the bandwidth is used. Through USB connection Kinect sensor provides a range of bandwidth for passing data.  High-resolution images send more data per frame and update less frequently, while lower-resolution images update more frequently, with some loss in image quality due to compression [11]. Table 1 specifies available formats for color stream data [24].

Table 1 Color formats and short description [24]

Color Format	Description
RGB	32-bit, linear X8R8G8B8-formatted color bitmaps
RgbResolution640x480Fps30 or
RgbResolution1280x960Fps12 or
YuvResolution640x480Fps15 color formats
YUV	16-bit, gamma-corrected linear UYVY-formatted color bitmaps
RawYuvResolution640x480Fps15 color data formats
YUV data is available only at the 640x480 resolution and only at 15 fps.
Bayer	32-bit, linear X8R8G8B8-formatted color bitmaps RawBayerResolution1280x960Fps12 or RawBayerResolution640x480Fps30 color formats

  
 To display the sensor camera view to screen we have used Color Frame class. All the image data and format are present in this class. We have used RgbResolution640x480Fps30 as the color format to achieve maximum display output. The image data is stored as a pixel array. Then a Writeable Bitmap is declared to store the pixel data and displayed through display source.

3.2   Tracking (Skeleton)

One of the best features of Microsoft Kinect for Windows SDK is Skeletal Tracking. By using the Infrared (IR) camera, the sensor can track people and follow body actions. The Skeletal Tracking method can recognize up to six persons present in the field view.
 
Figure 4. User detection by Kinect sensor [7]


Out of six persons the method can track up to two people in detail. Figure 6 shows the joint points of a human body detected by Kinect [17].
 
Figure 5 (a) Skeleton joints found by Microsoft Kinect [3],
 (b) Joint structure on a user


   The method can recognize or locate 20 joint points as skeleton of the person. These 20 joint points are the locations of different parts of the tracked person that in total make a human skeleton. Figure 5 shows the joint points of a human body detected by Kinect.

   To track human body joint point’s accurately Skeleton data is stored 3D position data for human skeleton. The data of each skeleton joint are stored as X, Y, and Z coordinates. These coordinates are measured in meters by default. Figure 6 shows the x, y, and z axis of the sensor.
 
Figure 6 Illustration of the skeleton space [14]


The above figure is a right-handed coordinate system in which the Kinect sensor is placed at the origin. The center point of the sensor is the origin of the coordinate meaning that the coordinate (0, 0, 0) lies at the middle of the sensor.
X-axis: The positive x-axis increases on the left side.
Y-axis: Positive y-axis is increased in upward and decreased in downward.
Z-axis: Positive z-axis is increased in the direction of Kinect sensor.
These coordinates can be determined by the following method:
JointType.Position.X
JointType.Position.Y
JointType.Position.Z [17]
The skeletal tracking has two different modes of tracking user. One is standing mode and other one is seated mode. In standing mode, the tracking algorithm can track all 20 joint positions of the user. Whereas the seated mode can track the 10 joint points of the upper body starting from head to Hip Center. Standing is the default mode for the tracking algorithm. Figure 7 shows the tracking joint points in standing and seated mode.
 
Figure 7 (a) Body joints (Standing and Seated Mode), 
 (b) Body joint points in seated mode.

    To successfully track and get the values the user must have to maintain a minimum distance from Kinect sensor. The minimum distance is 2.1 meter or 6.89 feet. Figure 8 indicates that the user would have to be at a minimum distance from the Kinect to be able to successfully track by Kinect [1].
 
Figure 8 Minimum distance for tracking [1] 

    As our one of the goal is to view the user size of the dress, we have used skeleton coordinates to calculate body measurement of the user. We have implemented an algorithm that measures the body width by taking the values of these skeleton coordinates. The algorithm calculates the body measurement by the coordinates of shoulderLeft, shoulderRight and shoulderCenter joint points. shoulderCenter, hipCenter are used to calculate the position of the user.

3.3   Gesture Recognition

In our final system we have used Gesture Recognition technology in the user interface. This gesture recognition enables the user to change the clothes the by swipe of their hand. It is very efficient as the user stands in a little distance from display or screen and cannot use touch-screen or other technology to change the cloths.
    To gesture recognition work successfully we have used a built-in class called Recognizer. This class recognizes the left Swipe and right Swipe when the user hand is swiped over the Kinect sensor. When right hand swipe is detected then next cloth is shown in screen. And when left hand swipe is detected then previous cloth is shown in screen.

3.4   User Interface

As this is a virtual mirror and it can be implemented anywhere from commercial cloth store to personal dressing mirror, there is no need of external display for the application interface. The virtual mirror is used to show the interface and it is controlled by gesture recognition feature of Kinect. Particularly swipe gesture will be implemented and will be distinguished by right and left swipe method. This swipe method can be tracked and implemented by using the Kinect sensor.  By using this feature user will be able to choose their desired clothes to display. Figure 11 shows the xml view of our user interface.
 Figure 9 User Interface of our system


3.4   Computation of two points

As stated earlier one of the main purposes of our system is to display preferred size of user clothing. For this computation we have used Kinect’s Skeleton Position method. We have created an algorithm that calculates the measurement between two shoulders by taking the values of left Shoulder and right Shoulder. Kinect sensor maps the skeleton values in 3D coordinate (X, Y, Z). We have mapped the coordinate in 2D by fixing the Z coordinate. To calculate the value, we have used the basic method of 2D system:
"W"=("Y" _"2" -"Y" _"1" )/("X" _"2" -"X" _"1"  )………………………………………..	(1)
"W" _"R" =("Y" _"LS" -"Y" _"CS" )/("X" _"LS" -"X" _"CS"  )…………………………………….	(2)
"W" _"L" =("Y" _"LS" -"Y" _"CS" )/("X" _"LS" -"X" _"CS"  )…………………………………….	(3)
Here,
  W = Distance of two coordinate points
  WL = Distance of left shoulder coordinate point
  WR = Distance of right shoulder coordinate point
   "Y" _"2" = next value of y-axis and "Y" _"1" = previous value of y-axis
  "X" _"2" = next value of x-axis and "X" _"1" = previous value of x- axis
  "Y" _"LS" = left shoulder y-axis value and "Y" _"RS" = right shoulder y-axis value
  "X" _"LS" = left shoulder x-axis value and "X" _"RS" = right shoulder x-axis value
  "Y" _"CS" = center shoulder y-axis value and "X" _"CS" = center shoulder x-axis value

     Using the above equation, we have calculated the measurement of right shoulder by calculating the coordinate distance between right Shoulder and center Shoulder. Then the measurement of left shoulder has been determined by calculating the coordinate distance between left Shoulder and center Shoulder.


4.0 EXPERIMENTAL EVALUATION

4.1   Testing

During the testing phase main attention was given to all the implemented functions and how the data was presented on different users. To evaluate the performance of the system, we have taken 20 test subjects (Users) and measured their body width. Table 2 holds the 20 subjects body measurement dataset.

Table 2 Dataset
Sl.	Name	Gender	Height	Kinect Value	Preferred Size
1	Nafis	Male	Medium	694	Large
2	Nahyan	Male	Medium	604	Medium
3	Monir	Male	Long	614	Medium
4	Nargis	Female	Medium	620	Semi-Large
5	Mahmudul Hoq	Male	Short	563	Small
6	Jaber	Male	Short	579	Medium Small
7	Arifin	Male	Short	589	Medium
8	Shuvo	Male	Semi-Short	594	Medium
9	Peash	Male	Medium	572	Small
10	Bijoy	Male	Medium	621	Medium
11	Arpita	Female	Short	562	Medium-large
12	Oni	Male	Medium	616	Medium
13	Oporna	Female	Short	561	-
14	Mursida	Female	Short	553	-
15	Nivrito	Male	Short	611	Medium
16	Hirok	Male	Short	642	Large
17	Adnan	Male	Short	669	Large
18	Arko	Male	Long	618	Medium
19	Mahabub	Male	Long	578	Medium
20	Shoib	Male	Medium	624	Large


    For testing purpose, we have instructed each of our subjects to stand in front of the Kinect sensor. After detecting the body measurement, we have noted the measurement to an excel sheet. After that we also noted their personal information such as name, gender and height. We have also asked about their preferred size when they go to shopping. We also noted their preferred size of clothes they wear. Moreover, we tested that our user static position is accurately working or not on each of the subject. Figure 10 shows the process of testing the system. 
 
(a)
 
(b)
 (c)

Figure 12 (a) Final system testing, (b) Before tracking State, (c) After Tracking State


4.2   Checking Value

After testing phase, we categorized all the values in three categories. 
    The categories are:
	Large 
	Medium
	Small
Based on these categories our system shows different size of garments. These sizes of garments are mainly dependent on system body measurement and the above mentioned categories.


5.0 RESULTS AND DISCUSSION

5.1   Performance

To ensure the performance of our algorithm, we compared height calculated from KSDK (Kinect SDK) in terms of subject’s original height. To this end, we recorded 30 frames (1s) of skeleton data from five subjects holding the standard standing position (T-pose) from a fixed distance to the Kinect sensor. The Kinect was placed 118 cm above the ground. Subject including both males and females were wearing T-shirt, jeans, and casual sneakers. We then calculated the standard deviation of each joint position for all the visible joints. Table 3 illustrates the accuracy of Kinect sensor measurement.

Table 3 Accuracy measurement of Kinect sensor

Shoulder center-to-feet height (cm)	Shoulder center-to-feet height (cm) KSDK	Accuracy (%)
163.4	160.2	98.04
181.5	178.4	98.29
176.2	170.5	96.76
172.3	170.8	99.13
165.5	161.4	97.52
 

    The results are shown in the Table above. The first column of the Table lists our manual measurement of the vertical distance between the floors to the mid-point of the shoulder. This is the shoulder height that our clothes-body fitting algorithm expects to cover the virtual clothes.

5.2   Result

Based on our proposed goals we have completed them. We have modified and advance the system and successfully created an environment where user can use a virtual mirror. The user can see the virtual dress and based on the skeleton measurement we can distinguish the user into different category and propose different dress for different categories of user. 
 
(a)
 
(b)
 
(c)
 
(d)

Figure 13 (a) Gesture Control, (b) Fix Position, (c) Measurement Detection using skeleton tracking. (d) Dynamic Dress

6.0 CONCLUSION

In this paper, we introduce a virtual dressing room application where avatar and cloth generation, real time tracking technologies up to an overview of comparable virtual try-ons. Subsequently a closer look on the technologies and frameworks that were used for the implementation of the virtual dressing room was taken. After this the different aspects of the design process up to the construction of the garment models was highlighted. This is followed by the implementation, describing the cloth colliders and the behavior of the garment, for instance. In the last section the tests were executed, also discussing the output, the appearance and the interaction with the virtual dressing room. Overall, the presented virtual dressing room seems to be a good solution for a quick, easy and accurate try-on of garment. The Microsoft Kinect offers the optimal technology for a successful implementation. Compared to other technologies like augmented reality markers or real-time motion capturing techniques no expensive configurations and time-consuming build-ups are required. From this point of view, it is an optimal addition for a cloth store. A simple setup of the system can also be assembled at home since the minimum requirements are a computer with a screen and a Kinect.


Acknowledgement

We are grateful to BRAC University for providing assistants to complete our project.


References

	Cheema, U., M. Rizwan, R. Jalal, F. Durrani, N. Sohail, “The Trend of online shopping in 21st century: Impact of enjoyment in tam model” Asian Journal of Empirical Research, vol. 3, no. 2, pp. 131-141.
	Zhao, L., J. Zhou, “Analysis on the Advantages and Disadvantages of clothing Networking Marketing” International Journal of Business and social science, vol. 6, no. 4(1), pp.147-151, (2015). 
	Habib, A.B., A. Asad and W.B. Omar. 2015, “Magic Mirror Using Kinect,” BRAC University.
	Giovanmi, S., Y.C. Choi, J. Huang, E.T. Khoo and K. Yin. 2012. “Virtual try-on using Kinect and HD Camera,” MIG-2012, vol. 7660, pp. 55-65.
	Presle, P. 2012. “A Virtual Dressing Room based on Depth Data,” Vienna Uninversity of Technology, Klosterneuburg, pp. 25-36.
	Lee, M. W. & R. Nevatia. February 2007. Body Part Detection for Human Pose Estimation and Tracking. Proceedings of the IEEE Workshop on Motion and VideoComputing.
	Gavrila, D. M. & L. S. Davis. June 1996. 3-D model-based tracking of humans in action: a multi-view approach. Proceedings of the 1996 Conference on Computer Vision and Pattern Recognition, San Francisco.  
	Du, H., P. Henry, X. Ren, M. Cheng, D. B. Goldman, S. M. Seitz, & D. Fox. September 2011. RGB-D Mapping: Using Depth Cameras for Dense 3D Modeling of Indoor Environments. Proceedings of the 13th international conference on Ubiquitous computing, Beijing, China. 
	Vera, L., J. Gimeno, I. Coma & M. Fernández. September 2011. Augmented Mirror: Interactive Augmented Reality based on Kinect. Proceedings of the 13th IFIP TC 13 International Conference on Human-Computer Interaction, Lisbon, Portugal.
	Vijayaraghavan, A., T.A. Induhumathi, J. Chopra A.R.N & K. Miracline R. April 2014. “A REAL Time Virtual Dressing Room Application Using OpenCV”, Anna University: Chennai 600 025.
	[online] http://www.dmi.unict.it/~battiato/CVision1112/Kinect.pdf.
	Billy, H.C.Y., M.C.K. Charles, N.K.K. Kit, W.Kenneth & A.Tam. “Intelligent Mirror for Augmented Fitting Room Using Kinect - Cloth Simulation”, Department of Computer Science, University of Hong Kong.
	Ziquan, L., & S. Zhao. December 2011. “Augmented Reality: Virtual fitting room using Kinect”, Department of Computer Science, School of Computing, National University of Singapore.
	[online] https://msdn.microsoft.com/en-us/library/jj131033.aspx.
	[online] H. Fairhead, “All About Kinect,”. Retrieved from http://www.i-programmer.info/babbages-bag/2003-kinect-the-technology-.html.
	Yolcu, G., S. Kazan, and C. Oz. 2014. “Real Time Virtual Mirror Using Kinect,” Baikan journal of Electrical & Computer Engineering, vol. 2, no. 2, pp. 75-78.
	[online] http://home.hit.no/~hansha/documents/microsoft.net/tutorials/ introduction%20to%20visual%20studio/Introduction%20to%20Visual%20Studio%20and%20CSharp.pdf.
	[online] Brenner, Pat. July 2013. “C99 library support in Visual Studio 2013”, Visual C++ Team Blog. Microsoft.
	Chai, D. and K. N. Ngan. 1999. Face Segmentation using Skin-Color Map in Videophone Applications, IEEE Transactions on Circuits and Systems for Video Technology, vol. 9, no. 4 and pp. 551-559. 
	Sur, A. 2013. Visual Studio 2012 and .NET 4.5 Expert Development Cookbook, vol.1, Chapter No. 1 “Introduction to Visual Studio IDE Features”.
	Guthrie, Scott. July 2007. “Nice VS 2008 Code Editing Improvements”.
	Guthrie, Scott. June 2007. “VS 2008 JavaScript IntelliSense”.
	Guthrie, Scott. July 2007. “VS 2008 Web Designer and CSS Support”. 
	[online] https://msdn.microsoft.com/en-us/library/jj131027.aspx.
	Kotan, M., and C. Oz. 2014. “Virtual Mirror with Virtual Human using Kinect Sensor,” 2nd International Symposium on Innovative Technologies in Engineering and Science, Karabuk University, Turkey, pp. 730-738.
	[online] http://tutorial.math.lamar.edu/Classes/DE/EulersMethod.aspx.
	[online] https://www.math.ksu.edu/math240/book/chap1/numerical.php.
	[online] http://www.myphysicslab.com/runge_kutta.html.
