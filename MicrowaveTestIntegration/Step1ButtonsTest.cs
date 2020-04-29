using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NUnit.Framework;
using NSubstitute; 

namespace MicrowaveTestIntegration
{
    [TestFixture]
    public class Step1ButtonsTest
    {
        private Button sut_powerButton;
        private Button sut_timeButton;
        private Button sut_startCancelButton;
        private Door door;

        private ICookController cook;
        private ILight light;
        private IDisplay display;
        private UserInterface ui; 

        [SetUp]
        public void Setup()
        {
            sut_powerButton = new Button();
            sut_timeButton = new Button();
            sut_startCancelButton = new Button();
            door = new Door();

            cook = Substitute.For<ICookController>();
            light = Substitute.For<ILight>();
            display = Substitute.For<IDisplay>();

            ui = new UserInterface(sut_powerButton, sut_timeButton, sut_startCancelButton, door, display, light, cook);
        }

        //Test af forbindelse mellem powerButton og UI
        [Test]
        public void PowerButton_Pressed_display_Received_ShowPower_50()
        {
            sut_powerButton.Press();

            display.Received().ShowPower(50);
        }

        [Test]
        public void PowerButton_Pressed_display_Received_ShowPower_100()
        {
            sut_powerButton.Press();
            sut_powerButton.Press();

            display.Received().ShowPower(100);      //Jeg er i tvivl om vi skal teste flere scenarier. Den bliver jo nulstillet på et tidspunkt? Men er det unit test som tester dette?
        }


        //Test af forbindelse mellem timeButton og UI
        [Test]
        public void TimeButton_Pressed_display_Received_ShowTime_1_0()
        {
            sut_powerButton.Press(); //Her bliver state sat til SETPOWER
            sut_timeButton.Press();

            display.Received().ShowTime(1, 0); //Tallene 1 og 0 kommer fra UserInterface klassen. Man har brugt WhiteBox til at kigge ind i klassen. Dog bruges der black box til at teste
        }

      
        [Test]
        public void TimeButton_Pressed_display_Received_ShowTime_2_0()
        {
            sut_powerButton.Press(); //Her bliver state sat til SETPOWER
            sut_timeButton.Press();  //Her bliver state sat til SETTIME
            sut_timeButton.Press();

            display.Received().ShowTime(2, 0); 
        }


        //Test af forbindelse mellem StartCancelButten og UI
        //SetPower
        //SetTimer
        //Cooking
        [Test]
        public void StartCancelButton_Pressed_light_Received_TurnOff()
        {
            sut_powerButton.Press(); //Her bliver state sat til SETPOWER
            sut_startCancelButton.Press();
            
            light.Received().TurnOff(); //Jeg er i tvivl om det er nok bare at tjekke for light? Eller skal man også tjekke for display?
        }

        [Test]
        public void StartCancelButton_Pressed_light_Received_TurnOn()
        {
            sut_powerButton.Press(); //Her bliver state sat til SETPOWER
            sut_timeButton.Press();  //Her bliver state sat til SETTIME
            sut_startCancelButton.Press();

            light.Received().TurnOn(); //Jeg er i tvivl om det er nok bare at tjekke for light? Eller skal man også tjekke for display?
        }

        [Test]
        public void StartCancelButton_Pressed_CookController_Received_Stop()
        {
            sut_powerButton.Press(); //Her bliver state sat til SETPOWER
            sut_timeButton.Press();  //Her bliver state sat til SETTIME
            sut_startCancelButton.Press();  //Her bliver state sat til COOKING
            sut_startCancelButton.Press();

            cook.Received().Stop(); //Jeg er i tvivl om det er nok bare at tjekke for cookingController? Eller skal man også tjekke for display?
        }



    }
}
