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
    //Tror vi får problemer med hensyn til dette på Jenkins. 
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

        //Test af forbindelse mellem power button og UI
        [Test]
        public void PowerButton_Pressed_UI_Received_OnPowerPressed_StateREADY()
        {
            sut_powerButton.Press();

            display.Received().ShowPower(50);
        }

        [Test]
        public void PowerButton_Pressed_UI_Received_OnPowerPressed_StateSETPOWER()
        {
            sut_powerButton.Press();
            sut_powerButton.Press();

            display.Received().ShowPower(100);      //Jeg er i tvivl om vi skal teste flere scenarier. Den bliver jo nulstillet på et tidspunkt? Men er det unit test som tester dette?
        }


        //Test af forbindelse mellem time button og UI
        [Test]
        public void TimeButton_Pressed_UI_Received_OnTimePressed_StateSETPOWER()
        {
            sut_powerButton.Press(); //Her bliver state sat til SETPOWER
            sut_timeButton.Press();

            display.Received().ShowTime(1, 0); //Tallene 1 og 0 kommer fra UserInterface klassen. Man har brugt WhiteBox til at kigge ind i klassen. Dog bruges der black box til at teste
        }

        //Test af forbindelse mellem time button og UI
        [Test]
        public void TimeButton_Pressed_UI_Received_OnTimePressed_StateSETTIME()
        {
            sut_powerButton.Press(); //Her bliver state sat til SETPOWER
            sut_timeButton.Press();  //Her bliver state sat til SETTIME
            sut_timeButton.Press();

            display.Received().ShowTime(2, 0); 
        }



        [Test]
        public void StartCancelButton_Pressed_UI_Received_OnStartCancelPressed()
        {
            sut_startCancelButton.Press();

           // ui.Received().OnStartCancelPressed();
        }
        
    }
}
