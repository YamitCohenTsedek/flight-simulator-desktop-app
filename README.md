# flight-simulator-desktop-app
# In this milestone we built a Gui interface (WPF app) which allows us to control our aircraft. We set up 2 TCP communication channels built into our code as follows:* Commands Channel - In this channel, we will connect as a customer to the flight simulator which will serve as a server. In this channel we will pass basic set commands to the flight simulator.<br/> • Channel Info - In this channel the simulator will connect as a customer to us, and send us the data about the plane the data will be defined in the generic_small.xml file. Thus, we have created a server that will listen to the aircraft data in particular Lon, Lat.
# The plane can be flown in 2 methods:
# • Manual - we have a joystick with which we can set the aileron, elevators, rudder, throttle. So that any change in the joystick will pass an appropriate set command in the Command channel.
# * Automatic - we have a field for writing set commands. Which after entering them we can send them in the Command channel.
# Regarding the flight monitor component - in this component, there are 2 buttons:
# • Connect button - a button that connects to the 2 channels, the Commands channel and the Info channel. It can be assumed that the connection to the channels is successful (so there is no obligation to check the correctness of the connection).
# • Settings button - This button allows you to change the settings related to operating the system. This window allows you to change the settings of the server's IP address as well as change the PORT communication channels.
# These settings are saved in the App.config file. We got the ApplicationSettingsModel.cs class which knows how to read these settings, and link them to suitable Properties.
# After changing the IP and PORT addresses, this data will be saved (however the reconnection of the communication channels will take place only after pressing the Connect button again or restarting the system).In addition, in the flight screen, we see a graph-like control. In this graph we show the flight path of our aircraft. Our flight path is determined by the data sent in the Info channel, from which we want to sample the Lat and lon values and display them in a graph. Create for us the UserControl called FlightBoard.xaml. (This component allows scrolling back and forth, so That there is no fear that the aircraft will go beyond the limits of the display).The route shown is the route taken by the aircraft.
# Control monitor-
# In this monitor we will have 2 control options, the transition from one option to another is by using TabControl.
# Manual control-
# In this component we implemented a joystick with which we can determine the values of aileron, elevators, rudder, throttle. We determined the values of the throttle and rudder using a slider which has values in the range [-1,1]. Called the Joystick. This control was created for us in the file Joystick.xaml, but we set the logic for determining the values of the aileron, elevators given the position of the button. Any change of one of the values will lead to the aircraft being updated by the Commands channel.
# Automatic control-
# This component contains a TextBox control that allows you to write Set commands in the Commands channel. (It can be assumed that the commands
# Integrity). The control contains 2 buttons:
# • OK button - executes all commands one after the other in the Commands channel when between each command there is a delay of 2 seconds so that we can see it in the simulator (this does not mean that the monitor
# Will be frozen while sending the information).
# We assumed that the OK button would be pressed only once for each command write, and there would be no simultaneous sending of 2 different sets of commands.
# • Clear Button - Cleans the command screen. (No need to clear the screen after pressing the OK.)
# When the user enters commands that have not yet reached the server, the background of the TextBox will be painted red bright, and after sending the information will return to white.(Without having to wait and wait for the data to be verified by the flight simulator).
