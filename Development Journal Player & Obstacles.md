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

-Then I made a script to rotate the spinning platforms
```csharp
public class spinner : MonoBehaviour
{
    public float xAngle = 0;
    public float yAngle = 0;
    public float zAngle = 0;
    public float rotateSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(xAngle * rotateSpeed, yAngle * rotateSpeed, zAngle * rotateSpeed);    
    }
}
```
*Figure 6. This script makes whatever object it is on to rotate in different directions depending on the variable values it is given*

- Lastly I made a script for the end goal of each level to take the player to the next level.
```csharp
public class DoorToNextScene : MonoBehaviour
{
    private int levelLoadDelay = 0;

    private void OnCollisionEnter(Collision other)
    {
        //when the player touches the door, the next level is loaded
        if (other.gameObject.tag == "Player")
        {
            Invoke("LoadNextLevel", levelLoadDelay);
        }
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
```
*Figure 7. This script checks if the player has collided with the door and if it has, it loads the next level*

### What creative or technical approaches did you use or try, and how did this contribute to the outcome?

- I added in a main menu and made multiple levels to test the physics within my game in different situations. In level 1 I tested movement, level 2 I tested jumping, level 3 I tested physics on small objects and on level 4 I tested player interactions on spinning obstacles.
- At the end of each level is a door that takes the player into the next level. 

### Did you have any technical difficulties? If so, what were they and did you manage to overcome them?

- One difficulty I had was with the spinning objects as it would either do nothing to the player or throw them completely off the level. To fix this I gave the spinners physics materials so that there was friction on the object causing it to do it's intended purpose of dragging the player with it as it moves

## Outcome

- [Gameplay Video Link](https://youtu.be/6Ohrfzpe-L4)

## Critical Reflection

### What did or did not work well and why?

- Something that worked well is the fact that I already had the levels and main menu built as I had previously worked on this game before it was a task.
- One thing that didm't go well was the player falling over. This is because I want a physics based platformer with interesting physics but that also means physics working weird such as the player falling over. This means I had to use rotation lock which limits the interesting physics I can use.

### What would you do differently next time?

- Next time i would look more at the examples within the task as some of the scripts were helpful when I checked them and solved some of my problems

## Bibliography

Technologies, U. (s.d.) Unity - Manual: Physic Material. At: https://docs.unity3d.com/2021.2/Documentation/Manual/class-PhysicMaterial.html (Accessed  25/10/2024).


