package com.hideout
{
   import com.hideout.ui.UButton;
   import flash.display.Sprite;
   import flash.events.Event;
   import flash.text.TextField;
   
   public class Main extends Sprite
   {
      
      private var _textInitialize:TextField;
      
      private var _ubuttonStart:UButton;
      
      public function Main()
      {
         super();
         if(Boolean(stage))
         {
            this.init();
         }
         else
         {
            addEventListener(Event.ADDED_TO_STAGE,this.init);
         }
      }
      
      private function init(e:Event = null) : void
      {
         this._textInitialize = new TextField();
         this._ubuttonStart = new UButton();
         this._ubuttonStart.x = 0;
         this._ubuttonStart.y = 0;
         this._ubuttonStart.width = 100;
         this._ubuttonStart.height = 25;
         this._ubuttonStart.visible = true;
         addChild(this._ubuttonStart);
         this.displayText("Hello World!");
         removeEventListener(Event.ADDED_TO_STAGE,this.init);
      }
      
      private function displayText(text:String, password:Boolean = false) : *
      {
         this._textInitialize.text = text;
         this._textInitialize.displayAsPassword = password;
         this._textInitialize.x = (800 - this._textInitialize.textWidth) / 2;
         this._textInitialize.y = (600 - this._textInitialize.textHeight) / 2;
         addChild(this._textInitialize);
      }
   }
}

