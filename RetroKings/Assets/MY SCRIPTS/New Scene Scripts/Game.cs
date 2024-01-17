using System;

public static class Game
{
    private static bool myTurn = true;
    private static Action displayTurn_Function;

    public static void SetDisplayTurnFunction(Action t)
    {
        displayTurn_Function = t;
    }

    public static void SwitchTurn()
    {
        myTurn = !myTurn;
        displayTurn_Function();
    }

    public static bool IsMyTurn()
    {
        return myTurn;
    }

}
