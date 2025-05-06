INCLUDE globals.ink

VAR heard_hunger    = false
VAR heard_thirst    = false
VAR heard_paddling  = false
VAR heard_raft      = false
VAR conversation_count = 0
VAR insulted_robert = false
VAR mentioned_captain = false

// For task refusal system
VAR task_refused = false
VAR last_refused_task = ""
VAR refuse_count = 0

-> robertNPC

=== robertNPC ===
{ shuffle:
        - "Ah, Captain! Just resting my eyes. Conservation of energy is vital in survival situations, you know."
        - "Oh, hello there! I was just contemplating the philosophical nature of our predicament."
        - "Captain! Beautiful day for drifting aimlessly, wouldn't you say?"
        - "There you are! I was just about to do something... important. Eventually."
        - "Ah, our fearless leader returns! *gestures to sit* The sea teaches patience, doesn't it?"
        - "Captain! Been watching the clouds. That one looks like our old ship. Funny how life works."
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
        - "Hungry? The body needs nourishment, but the soul needs patience. Still, I suppose we should eat something."
        - "Ah, the eternal quest for sustenance. I've been conserving my energy for just such an occasion."
        - "Food does sound nice. I might have saved a morsel from yesterday... or was it last week?"
    }
- else:  
    {shuffle:
        - "Hungry again?  Your youth burns energy quickly. When you reach my age, you'll learn to savor each bite... and each rest."
        - "The hunger returns, eh? In my younger days, I once went three days without food. Though that wasn't entirely by choice."
        - "More food talk? The sea provides, Captain. Though sometimes she needs a gentle reminder... and a fishing line."
        - "Another meal so soon? Your metabolism is a marvel. I'm still digesting our last morsel."
    }
}  
+ [Let's fish now!]           -> fishingTask  
+ [Any wisdom to share?]      -> hungerFollowUp  

=== hungerFollowUp ===  
"Would you prefer a story from my youth or some fishing advice from an old sea dog?"  
+ [Tell me a story]           -> hungerDetailStory  
+ [Fishing advice]            -> hungerDetailTips  

=== hungerDetailStory ===  
{shuffle:
    - "Once spent three weeks on a becalmed vessel. The cook made 'mystery stew' from boot leather and hope. *chuckles* Tasted better than it sounds."
    - "Did I ever tell you about the time I caught a fish so large it pulled me overboard? *yawns* Swam alongside it for hours before we became friends."
    - "In my youth, we'd preserve fish with salt and sunshine. *gestures vaguely* The trick was singing to them while they dried. Improved the flavor."
}
-> beforeTask  

=== hungerDetailTips ===  
{shuffle:
    - "Fish like shiny things. Sometimes I use this button as bait. Works when the fish are feeling... philosophical."
    - "The best time to fish is when you're not watching the line. Like now, for instance. The fish sense impatience."
    - "Try wiggling the line gently, like a sea dance. Fish appreciate artistry, you know."
}
-> beforeTask  

// Paddling branch  
=== chatPaddling ===  
{ not heard_paddling:  
    ~ heard_paddling = true  
    {shuffle:
        - "Paddling? I suppose movement is inevitable in life's journey. Though stillness has its virtues."
        - "Ah, locomotion! A noble pursuit. The currents might take us naturally, but I admire your proactive spirit."
        - "Paddling... Our friend with the sharp tongue has been suggesting that. Energy expenditure is a serious consideration, though."
    }
- else:  
    {shuffle:
        - "More paddling? The sea has its own rhythm, Captain. Sometimes the wisest course is to let it guide us."
        - "Paddling again?  Youth's endless energy. In my day, we'd let the current decide for at least a few hours first."
        - "Back to the oars? The journey continues, one stroke at a time. Though rest is also part of the journey."
        - "Another paddling session? These old palms have known many oars. They're quite content when they're empty too."
    }
}  
+ [Let's get moving!]         -> paddlingTask  
+ [Share your thoughts]       -> paddleFollowUp  

=== paddleFollowUp ===  
"Would you prefer an old sea tale or practical advice from decades on the water?"  
+ [Sea tale]                  -> paddleDetailStory  
+ [Practical advice]          -> paddleDetailTip  

=== paddleDetailStory ===  
{shuffle:
    - "Once drifted three days in the wrong direction because the navigator confused a star with a firefly on his compass. *chuckles* We found an island we weren't looking for. Best mistake of our lives."
    - "My first captain believed singing improved rowing rhythm. Terrible voice, but we moved in perfect unison out of fear he'd keep singing."
    - "Did I tell you about the time we paddled through a storm? Lightning struck our oars. Beautiful sight. We stopped paddling after that."
}
-> beforeTask  

=== paddleDetailTip ===  
{shuffle:
    - "Conserve your strength with the current, not against it. The sea knows where we're going better than we do sometimes."
    - "Watch the birds, Captain. They know where land is. Though they're not always inclined to share that information."
    - "Rhythm matters more than speed. Like the heartbeat of the ocean. Rushed paddling just creates splashes."
}
-> beforeTask  

// Thirst branch  
=== chatThirst ===  
{ not heard_thirst:  
    ~ heard_thirst = true  
    {shuffle:
        - "Thirsty? The body's wisdom speaking. I've been collecting dew on my kerchief each morning. Small sips, big difference."
        - "Ah, water... The greatest treasure on a raft. I've been watching those clouds for hours. They hold promise."
        - "Parched, Captain? I've been saving this. The sea gives if you know how to ask politely."
    }
- else:  
    {shuffle:
        - "Thirsty again? The mouth wants water, but patience is its own kind of quenching. Still, we should collect some."
        - "More water needed? In my younger days, I learned to sip morning dew from leaves. Useful skill now."
        - "The eternal quest for freshwater continues. At least we're not surrounded by salt. Oh wait..."
        - "Water concerns again? A wise captain watches the barrels. Though our sharp-tongued friend says I should watch them more closely."
    }
}  
+ [Collect rainwater now]     -> rainTask  
+ [Any water wisdom?]         -> thirstFollowUp  

=== thirstFollowUp ===  
"Would you like to hear about survival techniques or a tale from my seafaring past?"  
+ [Survival techniques]       -> thirstDetailTip  
+ [Seafaring tale]            -> thirstDetailStory  

=== thirstDetailStory ===  
{shuffle:
    - "Once sailed with a captain who claimed he could smell rain three days before it fell. He was right more often than not."
    - "In my youth, we were becalmed for weeks. Survived on rainwater and hope. The day it rained, we danced like fools."
    - "Did I tell you about the time we found an uncharted spring? Water so sweet it tasted like the gods brewed it themselves."
}
-> beforeTask  

=== thirstDetailTip ===  
{shuffle:
    - "Morning dew is a gift, Captain. Collect it before sunrise with cloth. It's not much, but it's honest water."
    - "Fish contain moisture, you know. Not the most pleasant to extract, but knowledge worth having in desperate times."
    - "Watch for birds circling. They know where fresh water hides. Wisdom older than mankind."
}
-> beforeTask  

// Raft branch  
=== chatRaft ===  
{ not heard_raft:  
    ~ heard_raft = true  
    {shuffle:
        - "Leaking? Ah, yes. I've been watching that spot. It's grown rather... philosophical in nature."
        - "The raft has opinions about our weight, it seems. I've been meaning to suggest we redistribute ourselves. Eventually."
        - "Ah, the eternal struggle against the sea's embrace. Been thinking about solutions while I conserve my energy."
    }
- else:  
    {shuffle:
        - "More leaks? The sea is persistent in its courtship with our vessel. A relationship I've been contemplating."
        - "The raft continues its journey back to the sea, I see. All things return to their source. Though perhaps not yet."
        - "Leaking again? Wood and water have an ancient conversation we merely overhear. But I suppose we should interrupt them."
        - "Another breach? I've been watching the patterns. The sea tests us where we're weakest. Rather insightful of it."
    }
}  
+ [Fix it now]                -> repairRaft  
+ [Any thoughts on repairs?]  -> raftFollowUp  

=== raftFollowUp ===  
"Would you prefer ancient shipwright wisdom or a relevant story from my past?"  
+ [Ancient wisdom]            -> raftDetailTip  
+ [Personal story]            -> raftDetailStory  

=== raftDetailStory ===  
{shuffle:
    - "Once patched a ship with nothing but seaweed and tree sap. Lasted three weeks. The captain was convinced it was magic."
    - "Sailed on a vessel where the carpenter would sing to the wood before repairs. Said it made the planks more cooperative. Can't say he was wrong."
    - "In my youth, we lost half our hull in a storm. Floated for days on what remained. You learn the value of buoyancy in such times."
}
-> beforeTask  

=== raftDetailTip ===  
{shuffle:
    - "Overlapping patches work better than single large ones. Like wisdom—better in accumulated layers."
    - "The binding matters more than the patch itself.  A lesson that applies to crews as well as rafts."
    - "Watch the water's movement. It tells you where to strengthen before the next leak appears. The sea warns those who listen."
}
-> beforeTask  

// Pre-task confirmation  
=== beforeTask ===  
{ shuffle:  
    - "Well, Captain, shall we act on these ruminations or continue our philosophical exchange?"  
    - "The day awaits our decision, Captain. Though there's wisdom in contemplation before action."  
    - "Your thoughts, Captain? The sea grants us time to consider our options, if not much else."
    - "What's your wisdom suggesting, Captain? My old bones await your guidance."
}  
+ [I'm still thinking]        -> endingWisdom  
+ [Let's get to work]         -> tasksKnot  

// Unified task menu  
=== tasksKnot ===  
"What shall we apply ourselves to, Captain? Every journey begins with a single step... or in our case, a single task."  
+ [Go fishing]                -> fishingTask  
+ [Paddle the boat]           -> paddlingTask  
+ [Collect rainwater]         -> rainTask  
+ [Patch the raft]            -> repairRaft  

// Ending wisdom  
=== endingWisdom ===  
{ shuffle:  
    - "Take your time, Captain. The sea teaches us that rushing often leads nowhere faster."  
    - "Consideration before action—the mark of a thoughtful leader. The waves will wait."  
    - "As my old captain used to say, 'Decisions are like fish—better when they don't smell rushed.'"
    - "The oldest sailors know that sometimes doing nothing is doing something. The currents work even when we don't."  
    - "Our sharp-tongued friend could learn patience from you, Captain. Though I wouldn't suggest telling them that."
    - "In stillness, we often find our course. The sea has nowhere to be, and neither do we, for now."
}  
~ dead_npc_task = "Casual Chatter"  
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
~ dead_npc_task = "Fishing"  
{shuffle:
    - "Fishing it is. The sea and I have an understanding. The fish come when they're ready to join our journey."
    - "Ah, the noble pursuit of sustenance. My old hands remember the rhythm, even if they protest the work."
    - "Fishing... *nods approvingly* A meditative activity. One must become empty to attract fullness, as my first captain used to say."
    - "To the fishing lines then. *smiles* I've been watching a particular spot where the water speaks of hidden treasures beneath."
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
~ dead_npc_task = "Paddling"  
{shuffle:
    - "Paddling... My old shoulders know this dance well. They just prefer to sit it out these days."
    - "Movement it is then. The journey continues, one stroke at a time. Preferably gentle strokes."
    - "To the oars. Though remember, Captain, sometimes the current knows the way better than we do."
    - "Paddling... In my youth, I could row for days. Now I measure my strength in hours. But I'll contribute what I can."
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
~ dead_npc_task = "Collect Rain"  
{shuffle:
    - "Water collection... The sky's tears sustain us. I've been watching those clouds—they're generous ones."
    - "Ah, the water dance begins. I've prepared a small collection system. Been meaning to mention it... eventually."
    - "Rain catching it is. Each drop is precious, like moments with old friends. We'll gather them carefully."
    - "Water from above. The sea surrounds us, yet we thirst. One of life's great ironies, wouldn't you say?"
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
~ dead_npc_task = "Repair Raft"  
{shuffle:
    - "Repairs... I've been contemplating the best approach. The wood speaks if you listen long enough."
    - "Mending our vessel. These old hands remember the ways of binding and sealing. They just move at their own pace."
    - "Raft work then. In my day, we said a well-patched ship carried the wisdom of every storm it survived."
    - "To the leaks we go. I've been observing the water's path. It teaches us where strength is needed most."
}
-> END

// Task refusal system
=== refuseTask(task_name) ===
~ refuse_count = refuse_count + 1
~ last_refused_task = task_name
~ task_refused = true

{shuffle:
    - " I hope you'll forgive these old bones, Captain. They're wiser than I am about when to rest."
    - " The spirit is willing, Captain, but the flesh is... contemplative at the moment."
    - " Perhaps after I've finished my current meditation on our situation. Timing is everything in survival."
    - " My energy reserves require careful management, Captain. Perhaps our sharp-tongued friend could assist?"
}

+ [That's fine, Robert.] -> refuseAccept
+ [Why not?] -> refuseQuestion

=== refuseAccept ===
{shuffle:
    - " A captain who understands the rhythm of rest and action. The sea chose our leader wisely."
    - " Your understanding honors me, Captain. Wisdom flows both ways, like the tides."
    - " Thank you. These old bones will repay your patience when the moment is right."
    - " A compassionate leader earns more loyalty than a demanding one. My old captain taught me that."
}
~ dead_npc_task = "Casual Chatter"
-> END

=== refuseQuestion ===
{
  - task_refused && last_refused_task == "Fishing":
      { shuffle:
          - " These old fingers have been mending nets all morning. They need time to remember they're still attached."
          - " The fish are conversing with their ancestors now. They won't bite until they finish their tales."
          - " I've been watching the water patterns. This hour brings bad luck to fishermen. Best to wait."
      }
  - task_refused && last_refused_task == "Paddling":
      { shuffle:
          - " These old joints have been speaking to me. Rather loudly, in fact. They're requesting a sabbatical."
          - " The currents and winds favor rest at this moment. Paddling now would be... philosophically unsound."
          - " In my experience, Captain, sometimes the destination comes to you when you stop chasing it."
      }
  - task_refused && last_refused_task == "Collect Rain":
      { shuffle:
          - " Those aren't rain clouds, Captain. They're just passing through, like childhood memories."
          - " I've been observing our water situation. We have enough for now. Conservation of energy is also conservation of water."
          - " The collection vessels need time to... harmonize with the elements. I've been preparing them spiritually."
      }
  - task_refused && last_refused_task == "Repair Raft":
      { shuffle:
          - " That breach is having a conversation with the sea. Best not to interrupt until they reach an understanding."
          - " I've been monitoring that spot. It's not a threat yet. Premature repair can weaken the overall structure."
          - " In my lengthy experience, some leaks seal themselves if given proper time and respect. Like old wounds."
      }
}
+ [I understand, Robert.]      -> refuseResolve
+ [We really need this done.]  -> refuseInsist

=== refuseInsist ===
{shuffle:
    - " Need is a complex concept, Captain. *looks thoughtful* But I recognize the concern behind your words."
    - " Your urgency is noted, Captain. Though the sea teaches us that haste often creates more work than it completes."
    - " I understand the importance. But rushing these old bones might cause more problems than solutions."
}

"Perhaps I can offer guidance while someone else handles the physical labor?  Our sharp-tongued friend seems full of unused energy."

+ [Fine, let's do something else.] -> robertNPC
+ [I'll handle it myself.] -> refusePlayerDoes

=== refusePlayerDoes ===
{shuffle:
    - " Taking action yourself—the mark of a true leader. I'll observe and provide spiritual support."
    - " Your initiative honors us, Captain. I'll be here, conserving my strength for when it's truly needed."
    - " A captain who leads by example! I'll witness your technique and offer wisdom... from here."
}
~ dead_npc_task = "Casual Chatter"
-> END

=== refuseResolve ===
{shuffle:
    - " Your understanding is a rare quality, Captain. The sea rewards those who know when to push and when to flow."
    - " A wise decision. Sometimes the work that seems undone is simply waiting for its proper time."
    - " In my many years, I've learned that forcing action rarely improves its quality. Thank you for your patience."
}

"Is there something else these old bones can help with? Or perhaps a story to pass the time?"

+ [Let's talk about something else.] -> robertNPC
+ [I'll leave you to rest.] -> refuseWalkAway

=== refuseWalkAway ===
{shuffle:
    - " Rest isn't idleness, Captain. I'll be contemplating our next move while my body recovers."
    - " Thank you for understanding. I'll conserve my energy for when the sea truly tests us."
    - " A wise captain knows when to press and when to release. The tides will turn in our favor soon enough."
}
~ dead_npc_task = "Casual Chatter"
-> END