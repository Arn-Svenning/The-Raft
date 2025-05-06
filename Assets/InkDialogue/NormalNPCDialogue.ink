// normalNPC.ink
INCLUDE globals.ink


// normalNPC.ink
-> normalNPC

=== normalNPC ===

{ shuffle:
    - "Well, well, if it isn’t my favorite distraction. Wanna jabber on, or actually point me at some work?"
    - "Back so soon? Save the pep talk or toss me a real task before I lose what’s left of my patience."
    - "Fancy bumping into you again. Shall we swap meaningless pleasantries, or do you finally have something useful for me?"
}
+ [Sure, let’s chat.]
    -> normalChatLoop
+ [Skip chat—tasks]
    -> normalTaskOptions

=== normalChatLoop ===
How’s the horizon treating you—smooth sailing or is your stomach doing flips?"
+ [Feeling like a boss!]
    -> normalChatResponse("awesome")
+ [I might actually throw up.]
    -> normalChatResponse("tired")
    
=== normalChatResponse(status) ===
{ status == "awesome":
    Fantastic—save some of that enthusiasm for when you’re hauling buckets."
- else:
    Boo‑hoo. Stop the dramatics and prove you can keep up."
}
~ normal_npc_task = "Casual Chatter"
-> END

=== normalTaskOptions ===
Alright, genius—what useless chore are we doing now?"
+ [Let's paddle the boat!]
    ~ normal_npc_task = "Paddling"
    Fine, I’ll row this leaky tub while you… oh right, stare into the horizon."
    -> END
+ [Let's go fishing!]
    ~ normal_npc_task = "Fishing"
    "Fishing—because apparently I’m a walking dinner bell for every fish in the sea."
    -> END
+ [Let's collect rainwater!]
    ~ normal_npc_task = "Collect Rain"
    "Rainwater, sure. Let’s gather nature’s tears since you can’t even quench your own thirst."
    -> END
+ [We need to fix the boat!]
    ~ normal_npc_task = "Repair Raft"
    "Holes again? Fantastic. Hand me the hammer before we all go under."  
    -> END
