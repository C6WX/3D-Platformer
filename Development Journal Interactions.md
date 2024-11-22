# 3D Platformer - Interactions

Fundementals of Game Development

Callum Wade 

2404781

## Research

### What sources or references have you identified as relevant to this task?
```markdown
-For this task, I researched into using velocity to move the player. This is because I needed to fling the player a distance with the catapult that I created for this task.
```

#### Sources
```markdown
# Documentation
For my game, I wanted to add a catapult, which requires me to use velocity. So to complete this task, I researched into using velocity from the rigidbody component in my code. The website I used to reseach this is the Unity documentation on Rigidbody.velocity. (Technologies, s.d.)

From reading through the website, I found out that to access velocity, I would need to first reference the rigidbody by using public Rigidbody rb; and rb = GetComponent<Rigidbody>();. Then you just need to use rb.velocity = new Vector3(0, 0, 0); and just change the numerical values to change the object's velocity as needed.

I found this website to be very simple to read with a good example of how to properly use velocity in a script.
# Game Source
A game that uses similar features as the ones added in this task is Apex Legends. Apex Legends is a first person shooter, battle royale created by Respawn Entertainment and published by Electronic Arts. (Apex Legends, 2019)

Apex Legends uses jump pads similarly to my game, however within my game, jump pads are used to complete the level and are required to beat the levels. But in Apex Legends jump pads are used as a tool to either get around easier or to gain a height advantage against enemies.

I found their use of jump pads to be very interesting as it shows how one things can be used for many different reasons within games.
```

## Implementation

### What was the process of completing the task? What influenced your decision making?

- First I started by adding in a jump pad asset into the game and then giving it a catapult tag.
- Then, in my PlayerMovement script, I implemented the catapult by adding two integers; catapultXValue and catapultYValue.

<br>

```csharp
    public int catapultXValue = 10;
    public int catapultYValue = 10;
```
*Figure 1. Shows the integers used for the catapult script*

- As I have previously referenced the rigidbody on the player, I don't need to do that again.
- So next i added a void OnTriggerEnter so that when the player touches an object with the tag "Catapult", it would add to the player's velocity based on the values of the two integers show above.

<br>

```csharp
void OnTriggerEnter(Collider other)
{
    if (other.gameObject.tag == "Catapult")
    {
        rb.velocity = new Vector3(catapultXValue, catapultYValue, 0);
    }
}
```
*Figure 2. Shows the script used to apply velocity to the player whenever they touch a object with the catapult tag*

- After that I added a speed pad into the game.
- To do this I started by adding to the void OnTriggerEnter with a script that increased the player's movement speed and then started a coroutine whenever the player touched an object tagged with "SpeedPad" 

<br>

```csharp
void OnTriggerEnter(Collider other)
{
    if (other.gameObject.tag == "Catapult")
    {
       rb.velocity = new Vector3(catapultXValue, catapultYValue, 0);
    }

    if (other.gameObject.tag == "SpeedPad")
    {
        moveSpeed = 5f;
        StartCoroutine(IncreaseSpeed());
    }
}
```
*Figure 3. Shows the OnTriggerEnter script with the new speed pad added to it*

- Then I added a coroutine so that after the player's speed increases, it waits a few seconds, before setting it back to the original speed.

<br>

```csharp
IEnumerator IncreaseSpeed()
{
    yield return new WaitForSeconds(10);
    moveSpeed = originalMoveSpeed;
}
```
*Figure 4. Shows the coroutine that waits 10 seconds before resetting the player's speed back to it's original speed*

### What creative or technical approaches did you use or try, and how did this contribute to the outcome?

- Did you try any new software or approaches? How did the effect development?

<br>

![onhover image description](https://beforesandafters.com/wp-content/uploads/2021/05/Welcome-to-Unreal-Engine-5-Early-Access-11-16-screenshot.png)
*Figure 2. An example of an image as a figure. This image shows where to package your Unreal project!.*

### Did you have any technical difficulties? If so, what were they and did you manage to overcome them?

- Did you have any issues completing the task? How did you overcome them?

## Outcome

Here you can put links required for delivery of the task, ensure they are properly labelled appropriately and the links function. The required components can vary between tasks, you can find a definative list in the Assessment Information. Images and code snippets can be embedded and annotated if appropriate.

- [Video Demonstration](https://www.youtube.com/watch?v=dQw4w9WgXcQ&ab_channel=RickAstley)
- [Game Build](https://samperson.itch.io/desktop-goose)

## Critical Reflection

### What did or did not work well and why?

- What did not work well? What parts of the assignment that you felt did not fit the brief or ended up being lacklustre.
- What did you think went very well? Were there any specific aspects you thought were very good?

### What would you do differently next time?

- Are there any new approaches, methodologies or different software that you wish to incorporate if you have another chance?
- Is there another aspect you believe should have been the focus?

## Bibliography

Technologies, U. (s.d.) Unity - Scripting API: Rigidbody.velocity. At: https://docs.unity3d.com/2020.1/Documentation/ScriptReference/Rigidbody-velocity.html (Accessed  22/11/2024).



