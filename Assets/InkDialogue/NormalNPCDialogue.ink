INCLUDE globals.ink

VAR heard_hunger    = false
VAR heard_thirst    = false
VAR heard_paddling  = false
VAR heard_raft      = false
VAR conversation_count = 0
VAR insulted_captain = false
VAR mentioned_robert = false

// For task refusal system
VAR task_refused = false
VAR last_refused_task = ""
VAR refuse_count = 0

-> normalNPC

=== normalNPC ===
{ shuffle:
        - "Oh look, it's the genius who got us stranded. What crisis needs solving now, 'Captain'?"
        - "Great, it's you again. Let me guess, something's wrong that needs my expertise to fix?"
        - "Wow, I'm honored. The almighty ex-captain graces me with their presence."  
        - "Finally decided to do some actual work, or just here to delegate again?"
        - "Oh joy, another visit from our fearless leader. What's falling apart now?"
        - "Nice to see you doing rounds while I do actual work. Brings back memories of your captaining."
}
+ [I'm starving.]          -> chatHunger
+ [Let's paddle.]          -> chatPaddling
+ [I'm parched.]           -> chatThirst
+ [This raft's leaking.]   -> chatRaft

// Hunger branch  
=== chatHunger ===  
{ not heard_hunger:  
    ~ heard_hunger = true  
    {shuffle:
        - "Hungry? Join the club. Your stomach's making noises that are attracting fish—maybe that's our new strategy?"
        - "Oh, the ex-captain needs feeding? I haven't eaten since yesterday, but sure, let's drop everything for your appetite."
        - "Food? I was wondering when you'd notice we're running low. Robert's been hoarding jerky under his shirt, by the way."
    }
- else:  
    {shuffle:
        - "Still moaning about food? Funny how your hunger always takes priority. Mine's just a hobby, I guess."
        - "Again with the hunger? Maybe if you'd salvaged more supplies before our ship became a coral reef..."
        - "The endless food saga continues. You know what doesn't help catch fish? Talking about not having fish."
        - "Another hunger complaint from the captain's log. Riveting. Meanwhile, I've been trying to fashion a better hook."
    }
}  
+ [Fine, let's fish!]           -> fishingTask  
+ [Spare me your drivel]       -> hungerFollowUp  

=== hungerFollowUp ===  
"What's it gonna be: brutal honesty or a fish-finding tip?"  
+ [Give it to me straight]     -> hungerDetailGritty  
+ [Just give me tips]          -> hungerDetailTips  

=== hungerDetailGritty ===  
{shuffle:
    - "Honestly? You couldn't catch a cold, let alone a fish. Your technique is painful to watch."
    - "Straight talk? You're the worst fisherman I've ever seen. And I once saw Robert try to catch fish with his bare hands."
    - "The truth? Every time you cast a line, fish swim to the opposite side of the ocean. It's like you repel them."
}
-> beforeTask  

=== hungerDetailTips ===  
{shuffle:
    - "Cast near the hull—lazy fish love shade. They're like the Robert of the ocean."
    - "Try jerking the line in short bursts. Mimics injured prey. Works better than your current 'scare everything away' approach."
    - "The red strips from your old captain's jacket make perfect lures. Fish can't resist that color."
}
-> beforeTask  

// Paddling branch  
=== chatPaddling ===  
{ not heard_paddling:  
    ~ heard_paddling = true  
    {shuffle:
        - "Paddling? Now you want to go somewhere? Let me guess, you saw a five-star resort on the horizon?"
        - "Oh good, movement. Because sitting in one spot getting crispy isn't depressing enough."
        - "Finally! I've been saying we should paddle northeast for days. Robert thinks we should just drift, the lazy bastard."
    }
- else:  
    {shuffle:
        - "Still dragging an oar? My arms are still recovering from your last brilliant navigation plan."
        - "What's wrong, bored with our current patch of endless ocean? Want a different view of nothing?"
        - "Back to rowing, are we? I was just thinking my blisters didn't have enough blisters."
        - "The rowing enthusiast returns! Let me guess, another 'current' you swear will take us to land?"
    }
}  
+ [Ugh, let's paddle!]         -> paddlingTask  
+ [Save the sermon]            -> paddleFollowUp  

=== paddleFollowUp ===  
"Want a cautionary mishap or a pro rowing tip?"  
+ [Mishap]                     -> paddleDetailMishap  
+ [Pro tip]                    -> paddleDetailTip  

=== paddleDetailMishap ===  
{shuffle:
    - "I tipped over once trying to follow your 'efficient' paddling technique. Felt like a champion, right?"
    - "Yesterday, Robert got his oar stuck in a mass of seaweed. We spun in circles for an hour while you napped."
    - "On my last vessel, the captain insisted on paddling through a jellyfish bloom. Still have the scars to show for it."
}
-> beforeTask  

=== paddleDetailTip ===  
{shuffle:
    - "Row with the current—your arms will thank you. Unlike me, the current doesn't hold grudges."
    - "Short, deep strokes are better than those wild splashing moves you call paddling."
    - "If you dip the blade fully before pulling, you'll get twice the distance for half the effort."
}
-> beforeTask  

// Thirst branch  
=== chatThirst ===  
{ not heard_thirst:  
    ~ heard_thirst = true  
    {shuffle:
        - "Thirsty? Saltwater's an option if you're into self-sabotage and painful death."
        - "Finally noticed our water situation, huh? I've been collecting condensation while you played captain."
        - "Water concerns from the ex-captain? Shocking. Maybe if we'd salvaged more containers before abandoning ship..."
    }
- else:  
    {shuffle:
        - "Still on about thirst? Try complaining to a cloud. Maybe it'll rain out of pity."
        - "Water again? I swear, between your thirst and Robert's complaining, it's like babysitting toddlers."
        - "The water saga continues. Maybe try talking less? Conserves moisture. Just friendly advice."
        - "You know what would help your thirst? If you'd helped me set up that condensation trap yesterday."
    }
}  
+ [Fine, rainwater it is]       -> rainTask  
+ [Spare me the sob story]      -> thirstFollowUp  

=== thirstFollowUp ===  
"Drought horror or barrel maintenance hack?"  
+ [Drought horror]             -> thirstDetailBad  
+ [Barrel hack]                -> thirstDetailTip  

=== thirstDetailBad ===  
{shuffle:
    - "I watched our barrels dry up while you lollygagged yesterday. Really entertaining show."
    - "Last week of my last voyage, water ran out. Crew started drinking their own... well, you get the idea."
    - "Robert accidentally knocked over our cleanest water yesterday. Claimed a fish jumped onto the raft and startled him."
}
-> beforeTask  

=== thirstDetailTip ===  
{shuffle:
    - "Keep barrels covered—less junk, more drinkable water. Revolutionary concept, I know."
    - "Line the collection barrels with cloth. Filters out the insects and Robert's hair."
    - "The morning dew collects on the tarp before sunrise. Extra source if you're not too lazy to collect it."
}
-> beforeTask  

// Raft branch  
=== chatRaft ===  
{ not heard_raft:  
    ~ heard_raft = true  
    {shuffle:
        - "Leaks again? My patch job squeaks louder than your excuses for getting us shipwrecked."
        - "The raft's leaking? No shit. I've been bailing water while you were having quality time with Robert."
        - "Finally noticed our floating home is more sieve than vessel? I've been saying we need better materials for days."
    }
- else:  
    {shuffle:
        - "Still bellyaching about holes? Get used to it. This heap of driftwood is held together by spite and bad knots."
        - "More leaks? This raft is falling apart faster than my respect for Robert's work ethic."
        - "Again with the leaks. Maybe if we'd salvaged better materials instead of your fancy captain's gear..."
        - "Another day, another leak. At this point, I'm considering just riding a barrel and leaving you two to sink."
    }
}  
+ [Patch it now]                -> repairRaft  
+ [Enough whining]             -> raftFollowUp  

=== raftFollowUp ===  
"Worst fail tale or quick seal hack?"  
+ [Fail tale]                  -> raftDetailFail  
+ [Seal hack]                  -> raftDetailTip  

=== raftDetailFail ===  
{shuffle:
    - "I once used seaweed as patch material—ended up leaking worse than before. Smelled fantastic though."
    - "Robert tried using his shirt to plug a hole. Now we have a bigger hole AND Robert's sunburned chest to look at."
    - "On my last vessel, the captain insisted on using rum to seal leaks. Waste of good rum AND we still sank. Remind you of anyone?"
}
-> beforeTask  

=== raftDetailTip ===  
{shuffle:
    - "Tar and cloth—sticky, smelly, but keeps water out better than your leadership kept our ship afloat."
    - "Overlap your patches. One layer does nothing, three layers might actually work."
    - "Heat the resin before applying. Makes it flow into the cracks better. Basic shipbuilding that apparently captains don't learn."
}
-> beforeTask  

// Pre-task confirmation  
=== beforeTask ===  
{ shuffle:  
    - "Okay… what now, 'Captain'? More brilliant insights or should we actually do something?"  
    - "Shall we actually work or just stand here admiring the endless ocean your navigation skills brought us to?"  
    - "Work or more captain wisdom? The suspense is killing me faster than dehydration."
    - "Tasks or talk? Decide before Robert wakes up and starts his 'helpful suggestions' routine."
}  
+ [Don't know]                 -> endingJoke  
+ [Show me the tasks]         -> tasksKnot  

// Unified task menu  
=== tasksKnot ===  
"What shall we tackle, oh mighty shipwreck survivor?"  
+ [Go fishing]                 -> fishingTask  
+ [Paddle the boat]            -> paddlingTask  
+ [Collect rainwater]          -> rainTask  
+ [Patch the raft]             -> repairRaft  

// Ending joke  
=== endingJoke ===  
{ shuffle:  
    - "Well, at least the sharks get a snack if we sink. Silver lining—they might eat Robert first."  
    - "On the bright side, I'm one tan line closer to perfection while we waste time chatting."  
    - "Remember: a patched raft is just a jealous cousin of the ship you managed to sink. No offense, 'Captain'."
    - "If all else fails, we'll live off seaweed and bad memories of your captaining decisions!"  
    - "Maybe we should just train the fish to push us to shore. Couldn't be worse than your navigation."
    - "Robert said we should build a message in a bottle. I suggested we put him in it instead."
}  
~ normal_npc_task = "Casual Chatter"  
-> END  

// Task leaves with 50% chance of refusal
=== fishingTask ===
{
  - (refuse_count == 0 || last_refused_task != "Fishing"):
      { shuffle:
          - -> actualFishingTask
          - -> refuseTask("Fishing")
      }
  - else:
      -> actualFishingTask
}

=== actualFishingTask ===
~ normal_npc_task = "Fishing"  
{shuffle:
    - "Fine, fishing it is. Try not to fall overboard this time. Actually, on second thought..."
    - "Alright, let's catch dinner. I'll handle the difficult part—you just try not to scare everything away."
    - "Fishing... again. At least it's better than listening to Robert's stories about his pet goldfish."
    - "Finally, something useful. I've got a new technique to try—just follow my lead for once, 'Captain'."
}
-> END  

=== paddlingTask ===
{
  - (refuse_count == 0 || last_refused_task != "Paddling"):
      { shuffle:
          - -> actualPaddlingTask
          - -> refuseTask("Paddling")
      }
  - else:
      -> actualPaddlingTask
}

=== actualPaddlingTask ===
~ normal_npc_task = "Paddling"  
{shuffle:
    - "To the oars then. Try to match my rhythm this time instead of paddling like you're swatting flies."
    - "Paddling it is. Let's see if we can make actual progress today, unlike yesterday's scenic tour of nothing."
    - "Fine, rowing time. My blisters thank you for your decision, 'Captain'. Let's hope your direction is better."
    - "About time we moved. I was starting to grow roots. Let's aim for that cloud formation—might mean land."
}
-> END  

=== rainTask ===
{
  - (refuse_count == 0 || last_refused_task != "Collect Rain"):
      { shuffle:
          - -> actualRainTask
          - -> refuseTask("Collect Rain")
      }
  - else:
      -> actualRainTask
}

=== actualRainTask ===
~ normal_npc_task = "Collect Rain"  
{shuffle:
    - "Water collection. Try not to contaminate it this time. Salt isn't a 'flavor enhancement' when you're dying."
    - "Finally, a sensible suggestion. I'll set up the main catch basin—you just try not to knock it over like yesterday."
    - "Water duty it is. Better than listening to Robert's theory about how fish are 'just like people but wetter'."
    - "Buckets out—let's catch every drop before it drifts off. Assuming you can hold a bucket without disaster."
}
-> END  

=== repairRaft ===
{
  - (refuse_count == 0 || last_refused_task != "Repair Raft"):
      { shuffle:
          - -> actualRepairRaft
          - -> refuseTask("Repair Raft")
      }
  - else:
      -> actualRepairRaft
}

=== actualRepairRaft ===
~ normal_npc_task = "Repair Raft"  
{shuffle:
    - "Time to patch this floating disaster again. Hand me the tools and try not to rock the boat. Literally."
    - "Repair duty. At least you noticed before we became submarine captains. I'll seal the main breach."
    - "Raft repair... my specialty at this point. Soon I'll be qualified to build luxury vessels from driftwood and spite."
    - "Time to seal these leaks. Hand me the tools before we sink lower than my expectations of your leadership."
}
-> END

// Task refusal system
=== refuseTask(task_name) ===
~ refuse_count = refuse_count + 1
~ last_refused_task = task_name
~ task_refused = true

{shuffle:
    - "Yeah... I don't think I'm going to do that right now. My enthusiasm tank is running on empty."
    - "You know what? I'm not feeling it. Maybe ask Robert? He's been looking bored for the past hour."
    - "How about no? I've already done more than my share today while you've been 'supervising'."
    - "Not happening, 'Captain'. Even ambitious people like me need breaks from constant crisis management."
}

+ [Okay, fine.] -> refuseAccept
+ [Why not?] -> refuseQuestion

=== refuseAccept ===
{shuffle:
    - "Smart move. Pushing me would've ended badly for both of us."
    - "Good. Glad we understand each other. I'm not one of your yes-men from the old ship."
    - "Finally learning how things work out here, huh? Progress."
    - "The former captain taking 'no' for an answer? Maybe there's hope for you yet."
}
~ normal_npc_task = "Casual Chatter"
-> END

=== refuseQuestion ===
{
  - task_refused && last_refused_task == "Fishing":
      { shuffle:
          - "Why not fish? Because I've been doing it for three hours while you were chatting with Robert. My turn to rest."
          - "My hands are still raw from the last fishing session. Maybe look at your own hands—soft as a cloud, I bet."
          - "I spotted a fin circling earlier. You go fishing if you're so hungry. I choose life."
      }
  - task_refused && last_refused_task == "Paddling":
      { shuffle:
          - "My shoulders feel like they're on fire. You try paddling non-stop since dawn."
          - "Because the current's against us. It's a waste of energy right now. Basic seamanship, 'Captain'."
          - "Robert promised he'd take the next shift. Go wake his lazy ass up instead of bothering me."
      }
  - task_refused && last_refused_task == "Collect Rain":
      { shuffle:
          - "There's barely any clouds. I'm not wasting time setting up collection for three drops of water."
          - "I already prepped the barrels earlier. Unless you want me to stand there staring at the sky?"
          - "The collection system is already optimized. More work now won't magically create rain."
      }
  - task_refused && last_refused_task == "Repair Raft":
      { shuffle:
          - "That leak is minor. We'll sink from your poor decisions long before that hole becomes a problem."
          - "We're running low on patch materials. I'm saving them for when we actually need them."
          - "I patched three holes at dawn. My fingers are still sticky with resin. Your turn, 'Captain'."
      }
}
+ [I understand.]                -> refuseResolve
+ [But we really need this done.] -> refuseInsist

=== refuseInsist ===
{shuffle:
    - "Oh, we 'really need this done'? I didn't realize. That changes everything. Except it doesn't."
    - "Playing the necessity card? Cute. Still not happening right now."
    - "Need and want are different things. And I need a break from constantly saving our sinking situation."
}

"Look, I'll help with {last_refused_task} later. Right now, pick something else or leave me alone."

+ [Fine, let's do something else.] -> normalNPC
+ [I'll handle it myself.] -> refusePlayerDoes

=== refusePlayerDoes ===
{shuffle:
    - "Now there's a shocking development—the captain actually doing manual labor! I'll alert the historians."
    - "By all means, show us how it's done. I'll just be over here, not holding my breath for success."
    - "Finally taking initiative! Maybe you'll learn something useful for a change."
}
~ normal_npc_task = "Casual Chatter"
-> END

=== refuseResolve ===
{shuffle:
    - "Good. Understanding boundaries—a novel concept for a captain. There's hope for you yet."
    - "Smart. Push too hard and I might decide to build my own raft. One without ex-captains on it."
    - "Appreciate that. Not everything needs to happen on your schedule. The ocean taught me patience."
}

"So, anything else you want to discuss, or should I go back to silently judging your leadership skills?"

+ [Let's talk about something else.] -> normalNPC
+ [I'll leave you alone.] -> refuseWalkAway

=== refuseWalkAway ===
{shuffle:
    - "Finally, some peace. Don't go too far though—something's bound to break again soon."
    - "Best decision you've made all day. Maybe check on Robert? Last I saw, he was 'organizing supplies'—code for napping."
    - "A captain who knows when to retreat. Impressive. There might be hope for our survival after all."
}
~ normal_npc_task = "Casual Chatter"
-> END