INCLUDE globals.ink

-> deadNPC

=== deadNPC ===
{ shuffle:
    - "Hey there, friend! Want to shoot the breeze or see what needs doing?"
    - "Hello again! Fancy a chat before we tackle some chores?"
    - "Well, look who’s here. Talk for a bit or get to work?"
}
+ [Sure, let’s chat.]  -> deadChatLoop
+ [Let’s get to it.]   -> deadTaskOptions

=== deadChatLoop ===
"How’re you feeling out here? Totally relaxed or itching for something to do?"
+ [Totally relaxed.]
    -> deadChatResponse("relaxed")
+ [Ready for action.]
    -> deadChatResponse("action")

=== deadChatResponse(status) ===
{ status == "relaxed":
    "Same here—nothing like a calm sea and a warm breeze."
- else:
    "You and me both. Let’s find something easy to pass the time."
}
~ dead_npc_task = "Casual Chatter"
-> END

=== deadTaskOptions ===
"What shall we do now?"
+ [Let’s go fishing!]  
    ~ dead_npc_task = "Fishing"
    "Perfect—I'll bait the hook and maybe we’ll snag a meal."
    -> END

+ [Let’s paddle the boat!]  
    ~ dead_npc_task = "Paddling"
    "Sure thing—grab an oar and I'll take the other side."
    -> END

+ [Collect rainwater]        
    ~ dead_npc_task = "Collect Rain"
    "Good call—let’s catch every drop we can."
    -> END

+ [Patch the raft]           
    ~ dead_npc_task = "Repair Raft"
    "Alright, let’s fix those leaks before they get worse."
    -> END
