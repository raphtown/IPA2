   M S B A M L   `   `   `  ����^  ZMicrosoft.Samples.Kinect.WpfViewers, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null�^clr-namespace:Microsoft.Samples.Kinect.WpfViewers;assembly=Microsoft.Samples.Kinect.WpfViewers#Microsoft.Samples.Kinect.WpfViewers  5   e    IPA2   IPA2.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   mmy^clr-namespace:Microsoft.Samples.Kinect.WpfViewers;assembly=Microsoft.Samples.Kinect.WpfViewers     5   e   -   5          � Title$  
MainWindow��5   	   $	��600��6   $	��800��6)   .��5      �� -   5        $��
MainCanvas��5      .+�5   
   =   7Microsoft.Samples.Kinect.WpfViewers.KinectSensorChooser  -   5        $��kinectSensorChooser1��5   @    �� Left$ 0��6!   
 �� Top$ 0��61   $	��778��6\   $	��561��6h   ;   5Microsoft.Samples.Kinect.WpfViewers.KinectColorViewer  5   
   -   5        $��kinectColorViewer1��5   W   $ 0��6   $ 0��6/   $	��530��6>   $	��413��6J   5   /Microsoft.Samples.Kinect.WpfViewers.ImageViewer   Kinect 5      ��  �� ElementName$ kinectSensorChooser1�� �� Path$ Kinect�>   8Microsoft.Samples.Kinect.WpfViewers.KinectSkeletonViewer  5	   
   -   5        $��kinectSkeletonViewer1��5	   N   $ 0��6"   $ 0��62   $	��413��6A   $	��530��6k    6w   �� $ kinectSensorChooser1��$ Kinect�6�     5
   
   -   5        $��kinectColorViewer2��5
   Y   $	 536��6   $ 0��61   $	��242��6@   $	��203��6L    5      �� $ kinectSensorChooser1��$ Kinect�;   5Microsoft.Samples.Kinect.WpfViewers.KinectDepthViewer  5   
   -   5        $��kinectDepthViewer1��5   O   $	 536��6   $	 209��61   $	��212��6B   $	��242��6i    6u   �� $ kinectSensorChooser1��$ Kinect��� 5   
   -   5        $��FirstContent��5   J   $	 100��6   $	 420��6"   $�� ��63   $��40��6>   $	��165��6^   �� 5   
   -	   5        $��
FirstLabel��5   O   $ 0��6   $	 420��6    $��Gesture��61   $��40��6C   $	��100��6a   5      5      