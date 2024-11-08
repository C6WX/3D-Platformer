# 3D Platformer - Polish

Fundementals of Game Development

Callum Wade 

2404781

## Research

### What sources or references have you identified as relevant to this task?
```markdown
- The thing that I found myself researching for this task is how to create particle systems in Unity and how to activate them with a script.
```

```Markdown
# Example

As I have done research regarding the audio identity and developing audio assets for this project in previous formative assignments. I wanted to look into specific Unreal and Wwise systems that will help create a more immersive experience. I wanted to focus on official documentation to improve my ability to learn new techniques without explicit instructions.

I also wanted a creative source to help develop the parachute audio assets and learn how it should function within the game’s narrative.
```

#### Sources
```markdown
# Documentation
For my game I wanted to create a particle system that looks like dust and plays whenever the player lands back on the ground. To do this I research into creating particle systems. This led me to looking at the unity documentation on particle systems. (Technologies, s.d.)

I found the particle system documentation to be helpful with creating my dust particle system as it explains everything that I need to know about particle systems. Using this I was able to change the colour, size and lifetime of the particle system to make it look like a dust affect.

I found this unity documentation to be difficult to read as the details on everthing to do with the partcile system is split into so many pages that it is hard to find what you need.

# Game Source
Call of Duty Black Ops 6 is a first person shooter developed by Treyarch and Raven Software, it feature lots of particle systems that are used for many different things within the game(Call Of Duty Black Ops 6, 2024).

Within the game, particle affects are used for so many different things such as gun fire, grenades and vehicle explosions, with most of the particle affects being unique from each other. 

The particle affects make the gameplay feel so much more realistic then if they werent in the game as grenades and smoke and explosions will be going off all around you as you play.

## Implementation

### What was the process of completing the task? What influenced your decision making?

- The first step I took for this task was to find audio for walking and landing to use online and then importing them into unity and I attatched them to audio sources on the main character to be activated within the code.
- I then started programming the walking audio so that whenever the player is moving and the walking audio isn't playing, it plays.

<br>

```csharp
        if (movement.magnitude > 0)
        {
            rb.AddForce(movement * moveSpeed);

            // Play walking audio only if it's not already playing
            if (!audioSources[moveAudioIndex].isPlaying)
            {
                audioSources[moveAudioIndex].Play();
                Debug.Log("Walking audio started.");
            }
        }
        else
        {
            if (audioSources[moveAudioIndex].isPlaying)
            {
                audioSources[moveAudioIndex].Stop();
                Debug.Log("Walking audio stopped.");
            }
        }
```
*Figure 1. Shows my script that plays the walking audio when the player is moving and the audio isn't already playing. If the player stops moving and the audio is playing, the audio stops playing.

- Next I worked on getting the landing audio to play when the player lands back on the ground.

```csharp
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = true;
            audioSources[groundAudioIndex].Play();
        }
    }
```
*Figure 2. This is the script I used to play the landing audio whenever the player collides with the ground*

- After getting the audio working, I got the particle system to player when the player lands.

```csharp
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = true;
            audioSources[groundAudioIndex].Play();
            TriggerDustAffect();
        }
    } 
```
*Figure 3. The script I used to activate the TriggerDustEffect method*

```csharp
   private void TriggerDustAffect()
    {
        // Ensure that dust particles are only triggered when the player actually lands
        if (dustParticleSystem != null)
        {
            // Trigger the dust effect only when grounded
            dustEmission.enabled = true;
            dustParticleSystem.Play();
            Debug.Log("Dust particle system triggered.");
        }
    }
```
*Figure 4. This script checks that the dust particle system exists then it enables the particle system then plays it.*

### What creative or technical approaches did you use or try, and how did this contribute to the outcome?

- The technical approach I took for this part of the project is using an array to access the different audios to play them.

```csharp
private AudioSource[] audioSources;
private int groundAudioIndex = 0; // Audio for landing
private int moveAudioIndex = 1; // Audio for walking/moving
```
```csharp
audioSources[moveAudioIndex].Play();
audioSources[moveAudioIndex].Stop();
```
```csharp
audioSources[groundAudioIndex].Play();
```
*Figure 5,6 and 7. Shows the code I used to access the audio sources from an array*
<br>

### Did you have any technical difficulties? If so, what were they and did you manage to overcome them?

The difficulty I had with this part of the project came along when I tried to use rays to check if the player was grounded to trigger the landing audio and particle system. Also implementing these into my game messed with the double jumping. This caused my script to have to get more complicated with the jumping while also trying to fix the audio and particle system. 
<br>
I managed to fix the jumping by adding a jumpCount variable which allows the player to jump till they reach the maximum amount of jumps instead of checking if the player is grounded and using that to allow the player to press space twice, which is what stopped working.
<br>
To deal with the audio and particle system problem, I removed all the raycasts and instead just used collisions to check if the player is colliding with an object that is tagged "Ground" and then activating the audio and particle system.

## Outcome

Here you can put links required for delivery of the task, ensure they are properly labelled appropriately and the links function. The required components can vary between tasks, you can find a definative list in the Assessment Information. Images and code snippets can be embedded and annotated if appropriate.

- [Example Video Link](https://www.youtube.com/watch?v=dQw4w9WgXcQ&ab_channel=RickAstley)
- [Example Repo Link](https://github.com/githubtraining/hellogitworld)
- [Example Build Link](https://samperson.itch.io/desktop-goose)

<iframe width="560" height="315" src="https://www.youtube.com/embed/dQw4w9WgXcQ?si=C4v0qHaYuEISAC01" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share" referrerpolicy="strict-origin-when-cross-origin" allowfullscreen></iframe>

*Figure 3. An example of an embedded video using a HTML code snippet.*

<iframe frameborder="0" src="https://itch.io/embed/2374819" width="552" height="167"><a href="https://bitboyb.itch.io/nephilim-resurrection">Nephilim Resurrection (BETA) by bitboyb</a></iframe>

*Figure 4. An example of a itch.io widget*

## Critical Reflection

### What did or did not work well and why?

- Something that went well is that I was able to use arrays to use multiple audio sources in one script. This worked well and also allowed me to learn more about using arrays while programming with them
- What didn't go well is the errors that occurred when trying to implement the audio and particle systems related to the jumping as this stopped the jumping from working and also made the audio and particles activate too soon.

### What would you do differently next time?

- Next time I would move on from a problem and come back to it later as I ended up getting annoyed after getting stuck on a bug that I wasn't able to fix till the next day. 

## Bibliography

Technologies, U. (s.d.) Unity - Manual: Particle systems. At: https://docs.unity3d.com/6000.0/Documentation/Manual/ParticleSystems.html (Accessed  08/11/2024).
<br>

## Declared Assets

Boost Panel - Download Free 3D model by Xane Myers (@Xane_MM) (2017) At: https://sketchfab.com/models/6b70e168530c40aeb237764766d6eb69/embed?autostart=1 (Accessed  08/11/2024).
<br>
Bounce Pad - Download Free 3D model by amftwg (2022) At: https://sketchfab.com/models/023a85a6a63e4d39937bf8cb3e38ae21/embed?autostart=1 (Accessed  08/11/2024).
<br>
Low Poly Rock Pack | 3D Environments | Unity Asset Store (s.d.) At: https://assetstore.unity.com/packages/3d/environments/low-poly-rock-pack-57874 (Accessed  08/11/2024).
<br>
Stylized Astronaut | Characters | Unity Asset Store (s.d.) At: https://assetstore.unity.com/packages/3d/characters/humanoids/sci-fi/stylized-astronaut-114298 (Accessed  08/11/2024).


