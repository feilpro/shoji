
using System.Collections;
using System.Collections.Generic;

public class Player{
    public Team Team { get; private set; }
    public SideBoard sideBoard { get; private set; }

    public Player(Team team)
    {
        this.Team = team;
        sideBoard = new SideBoard();
    }
}

public class SideBoard
{
    public Queue<Pawn> pawns = new Queue<Pawn>();
    public Queue<Spear> spears = new Queue<Spear>();
    public Queue<Horse> horses = new Queue<Horse>();
    public Queue<Silver> silver = new Queue<Silver>();
    public Queue<Golden> golden = new Queue<Golden>();
    public Queue<Tower> tower = new Queue<Tower>();
    public Queue<Bishop> bishops = new Queue<Bishop>();

}