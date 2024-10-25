# 3D Platformer - Player & Obstacles

Fundementals of Game Development

Callum Wade 

2404781

## Research

### What sources or references have you identified as relevant to this task?
```markdown
- One thing that I found myself reading into for this task is using physics materials to add friction to obstacles. 
```

#### Sources
```Markdown

# Documentation

Within my game I wanted the moving obstacles to push the player the way that they rotated, which led me to physics materials. Since I have never used physics materials, I used the unity documentation to research it. (Technologies, s.d.)

Using the website, I found out the what I needed was to change the static friction on the cyclinder to be minimum which caused the object to drag the player with it as I wanted it to do.

I found the website to be very helpful in teaching me how to use physics materials because it tells you every little thing about them from how to create a physics material to what each drop down menu item does. 

# Game Source
 Super Mario 3D World is a 3D platfromer game developed by Nintendo, it uses very similar mechanics to the 3D platformer I am making. (Super Mario 3D World, 2013).

 Within the game you are able to move the camera around the player and it is also a 3D platformer where the player can interact with obstacles and jump. 

 I found their use of these mechanics to be a great example for my game and future 3D platformer games.

```

## Implementation

### What was the process of completing the task? What influenced your decision making?

- I started this task by creating the movement script for the player.

<br>

```csharp
private void Moveplayer()
{
    //moves the player when WASD is pressed and changes the movement speed based the the value of the variable moveSpeed
    float moveHorizontal = Input.GetAxis("Horizontal");
    float moveVertical = Input.GetAxis("Vertical");
    Vector3 cameraForward = Vector3.Scale(_mainCamera.transform.forward, new Vector3(1, 0, 1)).normalized;
    Vector3 movement = (cameraForward * moveVertical + _mainCamera.transform.right * -moveHorizontal).normalized;        
    rb.AddForce(movement * moveSpeed);
}
```
*Figure 1. The script used to move the player by using the rigidbody on the player and adding force. This void is used in the update method*

- Next I implemented horizontal player rotation into the game

```csharp
private void Rotateplayer()
{
    xrValue = Input.GetAxis("Mouse X") * xRotateSpeed * Time.deltaTime;
    //allows the player to turn vertically by using the mouse
    transform.Rotate(0f, xrValue,0f);
}
```
*Figure 2. This script gets the horizontal mouse movement and rotates the player based on the mouse movement. This void is also called in the update.*

- Next I added double jumping into the game

```csharp
   //when the player is colliding with the ground, is grounded = true
   private void OnCollisionStay()
   {
       isGrounded = true;
   }
```
*Figure 3. Detects when the player is on the ground*

```csharp
 if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
 {
     rb.AddForce(jump * jumpForce, ForceMode.Impulse);
     isGrounded = false;
 }
```
*Figure 4. When space is pressed and the player is on the ground, the player can jump.*

-After adding jumping, I added camera movement.
```csharp
public Transform target;
public float rotationSpeed = 5f, zoomSpeed = 5f;
public float minZoomDistance = 2f, maxZoomDistance = 15f;

private float _rotationX = 0f, _rotationY = 0f, _currentZoomDistance = 10f;

private void Start()
{
    Cursor.lockState = CursorLockMode.Locked;
    if (target == null) target = GameObject.FindWithTag("Player").transform;
}

private void Update()
{
    float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
    float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;

    _rotationX -= mouseY;
    _rotationY += mouseX;
    _rotationX = Mathf.Clamp(_rotationX, -90f, 90f);

    Quaternion targetRotation = Quaternion.Euler(_rotationX, _rotationY, 0f);

    transform.position = target.position - targetRotation * Vector3.forward * _currentZoomDistance;
    transform.rotation = targetRotation;

    if (Input.GetKeyDown(KeyCode.Escape)) Cursor.lockState = CursorLockMode.None;

    float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
    if (scrollWheel != 0)
    {
        float zoomAmount = scrollWheel * zoomSpeed;
        UpdateZoomDistance(zoomAmount);
    }
}

private void UpdateZoomDistance(float zoomAmount)
{
    _currentZoomDistance = Mathf.Clamp(_currentZoomDistance - zoomAmount, minZoomDistance, maxZoomDistance);
}
```
*Figure 5. This script allows the camera to be rotated around the player by using the mouse*

### What creative or technical approaches did you use or try, and how did this contribute to the outcome?

- Did you try any new software or approaches? How did the effect development?

<br>

![onhover image description](https://beforesandafters.com/wp-content/uploads/2021/05/Welcome-to-Unreal-Engine-5-Early-Access-11-16-screenshot.png)
*Figure 2. An example of an image as a figure. This image shows where to package your Unreal project!.*

### Did you have any technical difficulties? If so, what were they and did you manage to overcome them?

- Did you have any issues completing the task? How did you overcome them?

## Outcome

Here you can put links required for delivery of the task, ensure they are properly labelled appropriately and the links function. The required components can vary between tasks, you can find a definative list in the Assessment Information. Images and code snippets can be embedded and annotated if appropriate.

- [Video Link](https://www.youtube.com/watch?v=dQw4w9WgXcQ&ab_channel=RickAstley)


## Critical Reflection

### What did or did not work well and why?

- What did not work well? What parts of the assignment that you felt did not fit the brief or ended up being lacklustre.
- What did you think went very well? Were there any specific aspects you thought were very good?

### What would you do differently next time?

- Are there any new approaches, methodologies or different software that you wish to incorporate if you have another chance?
- Is there another aspect you believe should have been the focus?

## Bibliography

Technologies, U. (s.d.) Unity - Manual: Physic Material. At: https://docs.unity3d.com/2021.2/Documentation/Manual/class-PhysicMaterial.html (Accessed  25/10/2024).


