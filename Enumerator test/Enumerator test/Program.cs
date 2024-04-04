// See https://aka.ms/new-console-template for more information


using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Text;
using Enumerator_test;
using SimpleGame;
using static System.Net.Mime.MediaTypeNames;

bool LPinsideInventory = false;
bool EXPinsideInventory = false;
bool ExitInsideInventory = false;
bool PotionInsideInventory = false;
bool SmallPotInsideInventory = false;
bool TinySeedInsideInventory = false;

Room startRoom = new Room("Start room");
Room corridor = new Room("Corridor");
Room kitchen = new Room("Kitchen");
Room laundryRoom = new Room("Laundry room");
Room Exit = new Room("Exit");
Room bedRoom = new Room("Bedroom");
Room bathRoom = new Room("Bathroom");
Room Cupboard = new Room("CupBoard");
Room Clauset = new Room("Clauset");
Room SmallHole = new Room("Small Hole");
Room TinyHole = new Room("TinyHole");
Room Tinydoor = new Room("TinyDoor");

startRoom.AddRoom(corridor);

Item lockpick = new Item("LockPick");
startRoom.AddItem(lockpick);
Room currentRoom = startRoom;

corridor.AddRoom(startRoom);
corridor.AddRoom(laundryRoom);
corridor.AddRoom(SmallHole);

SmallHole.AddRoom(corridor);
Item TinySeed = new Item("Tiny Seed");
SmallHole.AddItem(TinySeed);

laundryRoom.AddRoom(bedRoom);
laundryRoom.AddRoom(corridor);

Item plasticExplosives = new Item("Plastic Explosives");
laundryRoom.AddItem(plasticExplosives);

bedRoom.AddRoom(laundryRoom);
bedRoom.AddRoom(bathRoom);
bedRoom.AddRoom(Clauset);
bedRoom.AddRoom(kitchen);

Item ExitKey = new Item ("exitKey");
bedRoom.AddItem(ExitKey);

Clauset.AddRoom(bedRoom);
Clauset.AddRoom(TinyHole);

TinyHole.AddRoom(Tinydoor);

bathRoom.AddRoom(bedRoom);


kitchen.AddRoom(Exit);
kitchen.AddRoom(Cupboard);
kitchen.AddRoom(bedRoom);

Item Potion = new Item("Potion");
kitchen.AddItem(Potion);

Cupboard.AddRoom(kitchen);

Item SmallPotion = new Item("SmallPotion");
Cupboard.AddItem(SmallPotion);









Inventory inventory = new Inventory();
Game.GameState currentState = Game.GameState.ENTERING;

while (true)
{
    switch (currentState)
    {
        // main GameState used when entering a new or old room asking the player for input.
        case Game.GameState.ENTERING:
            Console.WriteLine($"\nYou're in the {currentRoom.GetRoomName()}");
            Thread.Sleep(500);
            Console.WriteLine("What do you want to do?");
            Thread.Sleep(500);
            Console.WriteLine("0 - Search for items");
            Thread.Sleep(500);
            Console.WriteLine("1 - Search for doors");
            Thread.Sleep(500);

            // uses the return from askfornumber, 
            int choice = AskForNumber(2);
            switch (choice){

                case 0:
                    currentState = Game.GameState.SEARCHFORITEMS;

                    break;
                case 1:
                    currentState = Game.GameState.SEARCHFORDOORS;
                    break;
                  }
            break;

            // searches for items in the currentroom, if none are there line1 will be displayed.
            // if not line 2 will be displayed and input will be reguired to add a item to the inventory.
        case Game.GameState.SEARCHFORITEMS:

            //shows all items in the current room.

            List<Item> items = currentRoom.GetItems();
            Console.WriteLine("You Search around");
            Thread.Sleep(2000);
            if (items.Count > 0)
            {
                Console.WriteLine("You find the following items in the room");

                Thread.Sleep(500);

                for (int i = 0; i < items.Count; i++){

                    Console.WriteLine($"{i} - {items[i].GetName()}");
                }
                Thread.Sleep(500);
                Console.WriteLine("Choose what item to pickup and add into your inventory");
                int itemSearch = AskForNumber(items.Count - 1);
                
                if (itemSearch < 0 ){
                    Console.WriteLine("you seem to grab hold of nothing.");
                    Thread.Sleep(2000);
                    Console.WriteLine("however, this made you realise how you are also nothing.");
                    Thread.Sleep(2000);
                    Console.WriteLine("you are sad.");
                    Thread.Sleep(2000);
                    Console.WriteLine("you rethink your choises.");
                    currentState = Game.GameState.ENTERING;
                }

                else{

                    Item gekozenItem = items[itemSearch];
                    inventory.AddItem(gekozenItem);
                    currentRoom.RemoveItem(gekozenItem);
                }

                if (currentRoom == startRoom){

                    LPinsideInventory = true;
                }

                else if (currentRoom == laundryRoom){

                    EXPinsideInventory = true;
                }

                else if (currentRoom == bedRoom){

                    ExitInsideInventory = true;
                }

                else if (currentRoom == kitchen){
                    PotionInsideInventory = true;
                }

                else if (currentRoom == Cupboard){

                    SmallPotInsideInventory = true;
                }

                else if (currentRoom == SmallHole){ 
                
                    TinySeedInsideInventory = true;
                }
            }

            else{
                Thread.Sleep(500);
                Console.WriteLine("You do your best to search but cant find anything of use");
                Thread.Sleep(500);
            }


            items = inventory.GetItems();
            for (int i = 0; i < items.Count; i++){
                Thread.Sleep(500);
                Console.WriteLine($"\nThe following items were added into the inventory {i} - {items[i].GetName()}\n");
                Thread.Sleep(500);
                Console.WriteLine($"Current Items in inventory {i} - {items[i].GetName()}\n");
            }



            currentState = Game.GameState.ENTERING;
            break;


        case Game.GameState.SEARCHFORDOORS:


            List<Room> choices = currentRoom.GetConnectedRooms();
            for (int i = 0; i < choices.Count; i++){
                Thread.Sleep(500);
                Console.WriteLine($"{i} - {choices[i].GetRoomName()}");
                Thread.Sleep(200);
                Console.WriteLine("go through this door?\n");
                
            }

            int choiceNumber = AskForNumber(choices.Count - 1);
            if (currentRoom == startRoom) {
                currentRoom = choices[choiceNumber];
                currentState = Game.GameState.ENTERING;
            }

            if (currentRoom == corridor && choiceNumber == 0)
            {

                currentRoom = choices[choiceNumber];
                currentState = Game.GameState.ENTERING;
                continue;
            }

            if (currentRoom == corridor && LPinsideInventory == false && choiceNumber == 1)
            {
                Console.WriteLine("the door seems locked");
                Thread.Sleep(500);
                currentState = Game.GameState.ENTERING;
            }

            else if (currentRoom == corridor && LPinsideInventory == true && choiceNumber == 1)
            {
                Thread.Sleep(500);
                Console.WriteLine("You unlock the door and proceed");
                Thread.Sleep(200);
                currentRoom = choices[choiceNumber];
                currentState = Game.GameState.ENTERING;
            }

            else if (currentRoom == corridor && SmallPotInsideInventory == false && choiceNumber == 2){
               
                Thread.Sleep(500);
                Console.WriteLine("you try to get into the small hole");
                Thread.Sleep(750);
                Console.WriteLine("however you notice only one part of your body would fit.");
                Thread.Sleep(750);
                Console.WriteLine("you decide to just not do that and rethink your choices");
                Thread.Sleep(750);
                currentState = Game.GameState.ENTERING;
            }

            else if (currentRoom == corridor && SmallPotInsideInventory == true && choiceNumber == 2){

                Thread.Sleep(500);
                Console.WriteLine("you try to get into the small hole");
                Thread.Sleep(750);
                Console.WriteLine("however you notice only one part of your body would fit.");
                Thread.Sleep(750);
                Console.WriteLine("you decide to however drink the smaller potion, as drinking random liquids has helped so far...");
                Thread.Sleep(2750);
                Console.WriteLine("you seem to start shrinking yet again");
                Thread.Sleep(750);
                Console.WriteLine("you proceed into the small hole");
                currentRoom = choices[choiceNumber];
                currentState = Game.GameState.ENTERING;
            }

            if (currentRoom == SmallHole && choiceNumber == 0){

                Thread.Sleep(500);
                Console.WriteLine("you move back outside the hole");
                Thread.Sleep(750);
                Console.WriteLine("you start growing back into your own size");
                Thread.Sleep(1750);
                Console.WriteLine("You look in your pants");
                Thread.Sleep(5750);
                Console.WriteLine("You start thinking *why cant there be an enlargement potion...*");
                Thread.Sleep(1750);
                Console.WriteLine("You shake the thought away and rethink options.");
                currentRoom = choices[choiceNumber];
                currentState = Game.GameState.ENTERING;
            }

            if (currentRoom == laundryRoom && EXPinsideInventory == false && choiceNumber == 1)
            {
                currentRoom = choices[choiceNumber];
                currentState = Game.GameState.ENTERING;
            }

            else if (currentRoom == laundryRoom && EXPinsideInventory == true && choiceNumber == 1){
                currentRoom = choices[choiceNumber];
                currentState = Game.GameState.ENTERING;
            }


            if (currentRoom == laundryRoom && EXPinsideInventory == false && choiceNumber == 0)
            {
                Thread.Sleep(750);
                Console.WriteLine("the door is locked");
                Thread.Sleep(750);
                currentState = Game.GameState.ENTERING;
            }

            else if (currentRoom == laundryRoom && EXPinsideInventory == true && choiceNumber == 0)
            {

                currentRoom = choices[choiceNumber];
                items = currentRoom.GetItems();

                Console.WriteLine("You place the explosives on the door...");
                Thread.Sleep(750);
                Console.WriteLine("the door handle blows off and you manage to walk through the hole");
                Thread.Sleep(750);
                currentState = Game.GameState.ENTERING;
            }

            else if (currentRoom == bedRoom && choiceNumber == 0 || currentRoom == bedRoom && choiceNumber == 1 || currentRoom == bedRoom && choiceNumber == 2)
            {

                currentRoom = choices[choiceNumber];
                currentState = Game.GameState.ENTERING;
            }

            if (currentRoom == bedRoom && choiceNumber == 3){

                currentRoom = choices[choiceNumber];
                currentState = Game.GameState.ENTERING;
            }

            if (currentRoom == Clauset && choiceNumber == 0){

                Console.WriteLine("you dont know why you went into the clauset");
                Thread.Sleep(750);
                Console.WriteLine("however...");
                Thread.Sleep(750);
                Console.WriteLine("you feel rather fruity coming out of it.");
                Thread.Sleep(750);

                currentRoom = choices[choiceNumber];
                currentState = Game.GameState.ENTERING;
            }

            if (currentRoom == Clauset && TinySeedInsideInventory == false && choiceNumber == 1){

                Console.WriteLine("You look into the tiny hole....");
                Thread.Sleep(750);
                Console.WriteLine("aint no way thats gonne be relevant to the story at all!...");
                Thread.Sleep(1750);
                Console.WriteLine("Right?");
                Thread.Sleep(3750);

                currentState = Game.GameState.ENTERING;
            }

            if (currentRoom == Clauset && TinySeedInsideInventory == true && choiceNumber == 1)
            {

                Console.WriteLine("You look into the tiny hole....");
                Thread.Sleep(750);
                Console.WriteLine("you have been digesting random stuff this whole time whats a single seed gonne do...");
                Thread.Sleep(1750);
                Console.WriteLine("Right?");
                Thread.Sleep(3750);
                Console.WriteLine("you swallow the seed and instantly shrink to the size of the hole");
                Thread.Sleep(3750);
                Console.WriteLine("you proceed into the hole and the way behind you collapses.");
                Thread.Sleep(750);
                Console.WriteLine("for dramatic effect");

                currentRoom = choices[choiceNumber];
                currentState = Game.GameState.ENTERING;
            }

            if (currentRoom == TinyHole && choiceNumber == 0){

                Console.WriteLine("you proceed through the doorway.");
                Thread.Sleep(3000);
                Console.WriteLine("the bright light blinds you");
                Thread.Sleep(3000);
                Console.WriteLine("you are however met with a small sparrow with a tiny stick");
                Thread.Sleep(4000);
                Console.WriteLine("Welcome Chosen one!");
                Thread.Sleep(2000);
                Console.WriteLine("take the commanding stick!");
                Thread.Sleep(2000);
                Console.WriteLine("as the bird hands you the stick he swoops you up with his beak onto his beak.");
                Thread.Sleep(5000);
                Console.WriteLine("let us make haste chosen one");
                Thread.Sleep(2000);
                Console.WriteLine("my brethren need to be saved from the evil crow sky fort!");
                Thread.Sleep(4000);
                Console.WriteLine("you fly away on the birds back");
                Thread.Sleep(2000);
                Console.WriteLine("confused...");
                Thread.Sleep(2000);
                Console.WriteLine("but happy");
                Thread.Sleep(1000);
                Console.WriteLine("as you fly over the road you see a civilian get clipped by a truck");
                Thread.Sleep(5000);
                Console.WriteLine("you see them flying into a wood shredder as they become fertilizer");
                Thread.Sleep(5000);
                Console.WriteLine("you say: cya wouldnt wanne be ya!");
                Thread.Sleep(3000);
                Console.WriteLine("Chosen Bird hero ending achieved (Exiting in 10 seconds.  )");
                Thread.Sleep(10000);
                Environment.Exit(0);
            }




            if (currentRoom == bathRoom && choiceNumber == 0){
                Thread.Sleep(500);
                Console.WriteLine("Eating mexican last night was the worst idea ever.");
                Thread.Sleep(750);
                Console.WriteLine("The toilet is in shambles and smells like a warzone.");
                Thread.Sleep(750);
                Console.WriteLine("you quickly leave before the owner returns home.");
                Thread.Sleep(1750);
                currentRoom = choices[choiceNumber];
                currentState = Game.GameState.ENTERING;
            }

            if (currentRoom == kitchen && ExitInsideInventory == false && choiceNumber == 0){

                Console.WriteLine("after noticing the door is locked shut, sheer panic sets in.");
                Thread.Sleep(6000);
                Console.WriteLine("\nafter some time passes you regain control from your mental breakdown.");
                Thread.Sleep(4000);
                Console.WriteLine("\nyou rethink your life choises.");
                Thread.Sleep(2000);
                currentState = Game.GameState.ENTERING;
            }

            if (currentRoom == kitchen && ExitInsideInventory == true && choiceNumber == 0){

                Console.WriteLine("You unlock the door and run towards your freedom!");
                Thread.Sleep(4000);
                Console.WriteLine("\nhowever life is cruel and you soon get to know this as a pickup truck clips you sending you into the nearest tree shredder");
                Thread.Sleep(4000);
                Console.WriteLine("\nyou become fertilizer.");
                Thread.Sleep(4000);
                Console.WriteLine("\nfirst ending achieved(Giving back to nature), second ending is waiting.");
                Thread.Sleep(4000);
                Environment.Exit(0);
            }

            if (currentRoom == kitchen && PotionInsideInventory == false && choiceNumber == 1){
                
                Thread.Sleep(500);
                Console.WriteLine("At first you try to climb into the cupboard like an idiot.");
                Thread.Sleep(3000);
                Console.WriteLine("\nYou look in the cupboard and see a smaller bottle");
                Thread.Sleep(1000);
                Console.WriteLine("\nyou try to reach but cant, you rethink your choises.");
                Thread.Sleep(1000);
                currentState = Game.GameState.ENTERING;
            }

            if (currentRoom == kitchen && PotionInsideInventory == true && choiceNumber == 1){

                Console.WriteLine("At first you try to climb into the cupboard like an idiot.");
                Thread.Sleep(1000);
                Console.WriteLine("\nYou drink the potion you found earlier");
                Thread.Sleep(1000);
                Console.WriteLine("\nyou shrink and just manage to make your way into the cupboard.");
                Thread.Sleep(1000);
                currentRoom = choices[choiceNumber];
                currentState = Game.GameState.ENTERING;
            }

            if (currentRoom == kitchen && choiceNumber == 2){

                Console.WriteLine("you make your way back into the bedroom.");
                Thread.Sleep(500);
                currentRoom = choices[choiceNumber];
                currentState = Game.GameState.ENTERING;

            }

            if (currentRoom == Cupboard && choiceNumber == 0) {

                Console.WriteLine("as you peer over the edge of the cupboard you consider jumping.");
                Thread.Sleep(500);
                Console.WriteLine("You jump like kratos in god of war when he is falling from the cliff.");
                Thread.Sleep(4000);
                Console.WriteLine("However you instantly regain your size your feet get pulled by the kitchen cabinet.");
                Thread.Sleep(4000);
                Console.WriteLine("You land face first into the floor");
                Thread.Sleep(3000);
                Console.WriteLine("Nosebleed achieved!");
                Thread.Sleep(2000);
                Console.WriteLine("you stand up and decide your next bloody move.");
                Thread.Sleep(1000);
                currentRoom = choices[choiceNumber];
                currentState = Game.GameState.ENTERING;
            }

            break;


        
        default:
            break;
    }
}


// used to translate the input of the player and when a number outside of the choises is given the player proceeds to slam into a wall.
int AskForNumber(int maxNumber){

    string input = Console.ReadLine();
    int output;

    int.TryParse(input, out output);
    if (output > maxNumber ){

        Console.WriteLine("you seem to step to far and proceed to slam your face into the walls, try again.");
        currentState= Game.GameState.ENTERING;
        return AskForNumber(maxNumber);
    }

    currentState = Game.GameState.ENTERING;
    return output;
}


