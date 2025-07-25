package com.hideout.ui
{
   import flash.display.SimpleButton;
   
   public class UButton extends SimpleButton
   {
      
      private var upColor:uint = 16763904;
      
      private var overColor:uint = 13434624;
      
      private var downColor:uint = 52479;
      
      private var size:uint = 80;
      
      public function UButton()
      {
         super();
         downState = new UButtonDisplayState(this.downColor,this.size);
         overState = new UButtonDisplayState(this.overColor,this.size);
         upState = new UButtonDisplayState(this.upColor,this.size);
         hitTestState = new UButtonDisplayState(this.upColor,this.size);
         useHandCursor = true;
      }
   }
}

