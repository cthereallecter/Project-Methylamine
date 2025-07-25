package com.hideout.ui
{
   import flash.display.Shape;
   
   public class UButtonDisplayState extends Shape
   {
      
      private var bgColor:uint;
      
      private var size:uint;
      
      public function UButtonDisplayState(bgColor:uint, size:uint)
      {
         super();
         this.bgColor = bgColor;
         this.size = size;
         this.draw();
      }
      
      private function draw() : void
      {
         graphics.beginFill(this.bgColor);
         graphics.drawRect(0,0,this.size,this.size);
         graphics.endFill();
         graphics.beginFill(16711935);
         graphics.drawRoundRect(0,0,this.size,this.size,45,45);
         graphics.endFill();
      }
   }
}

