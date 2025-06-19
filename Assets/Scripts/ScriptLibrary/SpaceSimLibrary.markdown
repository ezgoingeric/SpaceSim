# SpaceSimLibrary

## Source

https://github.com/Brprb08/orbital-control-simulator
https://github.com/mchrbn/unity-planetarium
https://github.com/Gloomglow/UnitySphericalGravity
https://github.com/notakamihe/Unity-Star-Systems-and-Galaxies
https://github.com/sotos82/SolarSystemSimulatorGame
https://github.com/Cawotte/ProceduralOrbits
https://github.com/Karth42/SimpleKeplerOrbits
https://github.com/Szugalew/Unity-Gravity-Simulation
https://github.com/hypothetical/Unity-NBody-Simulation
https://github.com/hypothetical/Unity-Spaceship-Controller
https://github.com/hypothetical/Unity-Orbit-Visualizer

# SpaceSimLibrary

## Overview

`SpaceSimLibrary.cs` is the core C# script for a Unity-based space simulation, integrating `PhysicsPlugin.dll` (from Orbital Control Simulator) for high-precision orbital mechanics. It supports a solar system and galaxy simulation with third-person starship navigation, targeting Unity 2023+, C#, URP, and a mid-range GTX laptop. The library handles Newtonian gravity, stable orbits, JSON-driven data, hybrid scaling, and floating origin, addressing performance, precision, and usability concerns. Optional scripts extend functionality while preserving the core library’s accuracy.

## Core Functionality

- **Physics**: Uses `PhysicsPlugin.dll` with a Dormand–Prince 5(4) integrator for double-precision orbit propagation and Newtonian gravity (\( F = GMm/r^2 \)).
- **Orbits**: Propagates stable orbits (e.g., Charon around Pluto, Mercury around Sun), supporting non-orbital paths (e.g., slingshots).
- **Navigation**: Implements third-person starship control.
- **Data**: Parses JSON for celestial body data (positions, velocities, masses), supporting NASA-verified data.
- **Rendering**: Applies hybrid scaling (\((k/d\_\text{object})^{0.5}\)) for visuals, using simple meshes (spheres).
- **Precision**: Uses double-precision and floating origin for large-scale (50 AU) simulations.
- **Performance**: Leverages DLL’s real-time execution for GTX laptop compatibility, avoiding PhysX collisions.

## Optional Scripts

- **CelestialCoordinates.cs**: Initializes celestial positions (altitude, azimuth, distance in AU, ~1° accuracy) based on Earth location and time (from unity-planetarium).
- **GravityAttractor.cs**: Applies spherical gravity for planetary interactions (from UnitySphericalGravity).
- **StarSystemGenerator.cs**: Generates procedural star systems with randomized planets (from Unity-Star-Systems-and-Galaxies).
- **HUDReadout.cs**: Displays object details (name, mass, distance) for HUD integration (from Unity-Star-Systems-and-Galaxies).
- **OrbitCalculator.cs**: Pre-computes Keplerian orbits using a Runge-Kutta integrator (from SolarSystemSimulatorGame).
- **SpacecraftController.cs**: Manages spacecraft navigation with Leapfrog integration (from SolarSystemSimulatorGame).
- **OrbitGenerator.cs**: Generates procedural orbits for planets around a sun (from ProceduralOrbits).
- **OrbitVisualizer.cs**: Renders faint orbital paths for visual feedback (from ProceduralOrbits).
- **KeplerOrbitMover.cs**: Simulates 2-body Keplerian orbits for celestial bodies (from SimpleKeplerOrbits).
- **KeplerOrbitLineDisplay.cs**: Renders faint orbital paths using LineRenderer (from SimpleKeplerOrbits).
- **GravityCalculator.cs**: Computes gravitational forces between celestial bodies (from Unity-Gravity-Simulation).
- **OrbitUpdater.cs**: Updates celestial body positions based on gravitational forces (from Unity-Gravity-Simulation).
- **NBodySimulator.cs**: Simulates N-body gravitational interactions (from Unity-NBody-Simulation).
- **SpaceshipController.cs**: Provides third-person spaceship navigation (from Unity-Spaceship-Controller).
- **OrbitPathRenderer.cs**: Renders orbital paths to reduce visual clutter (from Unity-Orbit-Visualizer).

## Requirements

- **Unity**: 2023+ with URP.
- **Hardware**: Mid-range GTX laptop (Windows 64-bit for DLL compatibility).
- **Dependencies**: `PhysicsPlugin.dll` in `Assets/Plugins`, JSON file with celestial data.
- **Optional**: ECS/DOTS for galaxy-scale performance, custom shaders for sub-pixel rendering, UI system for HUD/minimap.

## Setup Instructions

1. **Install PhysicsPlugin.dll**:
   - Copy `PhysicsPlugin.dll` from `Orbital-Control-Simulator/Assets/Plugins` to `Assets/Plugins`.
   - Include `Assets/Plugins/Source` for recompilation if needed (e.g., Visual Studio for Windows 64-bit).
2. **Add Core Library**:
   - Place `SpaceSimLibrary.cs` in `Assets/Scripts/ScriptLibrary`.
   - Attach to a GameObject (e.g., `GameManager`) in your Unity scene.
3. **Prepare JSON Data**:
   - Create a JSON file (e.g., `celestialData.json`):
     ```json
     [
       {
         "name": "Sun",
         "position": [0.0, 0.0, 0.0],
         "velocity": [0.0, 0.0, 0.0],
         "mass": 1.989e30,
         "baseSize": 1.0
       },
       {
         "name": "Mercury",
         "position": [5.79e10, 0.0, 0.0],
         "velocity": [0.0, 47.87e3, 0.0],
         "mass": 3.285e23,
         "baseSize": 0.1
       }
     ]
     ```
   - Assign to the `jsonData` field in the Unity Inspector.
4. **Configure Player**:
   - Assign a player GameObject (e.g., starship) to the `playerTransform` field.
5. **Set Up Rendering**:
   - Ensure celestial bodies use simple sphere meshes (PrimitiveType.Sphere).
   - Add LineRenderer for orbital paths (opacity ~0.2–0.3) and shaders for sub-pixel rendering (not included).
6. **Add Optional Scripts** (as needed):
   - Place optional scripts in `Assets/Scripts/ScriptLibrary/Optional`.
   - Follow integration instructions below.

## Optional Script Integration

### CelestialCoordinates.cs

- **Purpose**: Initialize celestial positions with ~1° accuracy using geocentric coordinates.
- **Integration**:
  - Add `CelestialCoordinates.cs` to `Assets/Scripts/ScriptLibrary/Optional`.
  - Modify `SpaceSimLibrary.cs`’s `Start`:
    ```csharp
    foreach (var body in celestialBodies) {
        Vector3 geoPos = CelestialCoordinates.GetPlanetPosition(body.name, latitude, longitude, System.DateTime.Now);
        body.position = ConvertToUnityPosition(geoPos);
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        body.transform = go.transform;
        body.transform.position = new Vector3((float)body.position[0], (float)body.position[1], (float)body.position[2]);
    }
    private double[] ConvertToUnityPosition(Vector3 geoPos) {
        double distance = geoPos.z * 1.496e11; // AU to meters
        double altitudeRad = geoPos.x * Mathf.Deg2Rad;
        double azimuthRad = geoPos.y * Mathf.Deg2Rad;
        double x = distance * Mathf.Cos(altitudeRad) * Mathf.Cos(azimuthRad);
        double y = distance * Mathf.Sin(altitudeRad);
        double z = distance * Mathf.Cos(altitudeRad) * Mathf.Sin(azimuthRad);
        return new double[] { x, y, z };
    }
    ```
  - Add `public double latitude, longitude;` to `SpaceSimLibrary.cs`.

### GravityAttractor.cs

- **Purpose**: Apply planetary gravity when the player is near a celestial body.
- **Integration**:
  - Add `GravityAttractor.cs` to `Assets/Scripts/ScriptLibrary/Optional`.
  - Modify `UpdatePlayerMovement` in `SpaceSimLibrary.cs`:
    ```csharp
    void UpdatePlayerMovement() {
        CelestialBody nearest = null;
        double minDistance = double.MaxValue;
        Vector3d playerPos = new Vector3d(playerTransform.position.x, playerTransform.position.y, playerTransform.position.z);
        foreach (var body in celestialBodies) {
            double distance = Vector3d.Distance(playerPos, new Vector3d(body.position[0], body.position[1], body.position[2]));
            if (distance < minDistance) {
                minDistance = distance;
                nearest = body;
            }
        }
        if (nearest != null && minDistance < 1e6) {
            Vector3d planetPos = new Vector3d(nearest.position[0], nearest.position[1], nearest.position[2]);
            Vector3d gravityForce = GravityAttractor.CalculateGravity(playerPos, planetPos, 1.0, nearest.mass);
            playerTransform.position += (Vector3)gravityForce * Time.deltaTime;
        }
        float moveSpeed = 1000f;
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        playerTransform.position += playerTransform.TransformDirection(input) * moveSpeed * Time.deltaTime;
    }
    ```

### StarSystemGenerator.cs

- **Purpose**: Generate procedural star systems if JSON data is unavailable.
- **Integration**:
  - Add `StarSystemGenerator.cs` to `Assets/Scripts/ScriptLibrary/Optional`.
  - Modify `Start` in `SpaceSimLibrary.cs`:
    ```csharp
    void Start() {
        if (jsonData != null) {
            celestialBodies = new List<CelestialBody>(JsonUtility.FromJson<CelestialBody[]>(jsonData.text));
        } else {
            StarSystemGenerator generator = gameObject.AddComponent<StarSystemGenerator>();
            celestialBodies = generator.GenerateStarSystem("TestSystem", 5);
        }
        foreach (var body in celestialBodies) {
            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            body.transform = go.transform;
            body.transform.position = new Vector3((float)body.position[0], (float)body.position[1], (float)body.position[2]);
        }
    }
    ```

### HUDReadout.cs

- **Purpose**: Display object details (name, mass, distance) for HUD integration.
- **Integration**:
  - Add `HUDReadout.cs` to `Assets/Scripts/ScriptLibrary/Optional`.
  - Attach to a GameObject and assign `SpaceSimLibrary` and a UI Text component:
    ```csharp
    HUDReadout readout = gameObject.AddComponent<HUDReadout>();
    readout.simLibrary = this;
    readout.hudText = GameObject.Find("HUDText").GetComponent<Text>();
    ```

### OrbitCalculator.cs

- **Purpose**: Pre-compute Keplerian orbits for celestial bodies using NASA data.
- **Integration**:
  - Add `OrbitCalculator.cs` to `Assets/Scripts/ScriptLibrary/Optional`.
  - Modify `Start` in `SpaceSimLibrary.cs`:
    ```csharp
    void Start() {
        if (jsonData != null) {
            celestialBodies = new List<CelestialBody>(JsonUtility.FromJson<CelestialBody[]>(jsonData.text));
            OrbitCalculator calculator = gameObject.AddComponent<OrbitCalculator>();
            celestialBodies = calculator.InitializeKeplerOrbits(celestialBodies);
        }
        foreach (var body in celestialBodies) {
            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            body.transform = go.transform;
            body.transform.position = new Vector3((float)body.position[0], (float)body.position[1], (float)body.position[2]);
        }
    }
    ```

### SpacecraftController.cs

- **Purpose**: Enhance navigation with Leapfrog-integrated spacecraft controls.
- **Integration**:
  - Add `SpacecraftController.cs` to `Assets/Scripts/ScriptLibrary/Optional`.
  - Attach to the player GameObject:
    ```csharp
    SpacecraftController controller = playerTransform.gameObject.AddComponent<SpacecraftController>();
    controller.simLibrary = this;
    ```
  - Requires `GravityAttractor.cs` for gravitational calculations.

### OrbitGenerator.cs

- **Purpose**: Generate procedural orbits for planets around a central sun.
- **Integration**:
  - Add `OrbitGenerator.cs` to `Assets/Scripts/ScriptLibrary/Optional`.
  - Modify `Start` in `SpaceSimLibrary.cs`:
    ```csharp
    void Start() {
        if (jsonData != null) {
            celestialBodies = new List<CelestialBody>(JsonUtility.FromJson<CelestialBody[]>(jsonData.text));
        } else {
            OrbitGenerator generator = gameObject.AddComponent<OrbitGenerator>();
            celestialBodies = generator.GenerateOrbits("TestSystem", 5);
        }
        foreach (var body in celestialBodies) {
            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            body.transform = go.transform;
            body.transform.position = new Vector3((float)body.position[0], (float)body.position[1], (float)body.position[2]);
        }
    }
    ```

### OrbitVisualizer.cs

- **Purpose**: Render faint orbital paths to reduce visual clutter.
- **Integration**:
  - Add `OrbitVisualizer.cs` to `Assets/Scripts/ScriptLibrary/Optional`.
  - Attach to each celestial body’s GameObject:
    ```csharp
    foreach (var body in celestialBodies) {
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        body.transform = go.transform;
        body.transform.position = new Vector3((float)body.position[0], (float)body.position[1], (float)body.position[2]);
        OrbitVisualizer visualizer = go.AddComponent<OrbitVisualizer>();
        visualizer.body = body;
    }
    ```

### KeplerOrbitMover.cs

- **Purpose**: Simulate 2-body Keplerian orbits for celestial bodies.
- **Integration**:
  - Add `KeplerOrbitMover.cs` to `Assets/Scripts/ScriptLibrary/Optional`.
  - Attach to each orbiting body’s GameObject and assign the attractor:
    ```csharp
    foreach (var body in celestialBodies) {
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        body.transform = go.transform;
        body.transform.position = new Vector3((float)body.position[0], (float)body.position[1], (float)body.position[2]);
        KeplerOrbitMover mover = go.AddComponent<KeplerOrbitMover>();
        mover.attractor = celestialBodies.Find(b => b.name == "Sun"); // Example: Sun as attractor
        mover.velocity = new Vector3d(body.velocity[0], body.velocity[1], body.velocity[2]);
    }
    ```

### KeplerOrbitLineDisplay.cs

- **Purpose**: Render faint orbital paths using LineRenderer.
- **Integration**:
  - Add `KeplerOrbitLineDisplay.cs` to `Assets/Scripts/ScriptLibrary/Optional`.
  - Attach to each celestial body’s GameObject:
    ```csharp
    foreach (var body in celestialBodies) {
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        body.transform = go.transform;
        body.transform.position = new Vector3((float)body.position[0], (float)body.position[1], (float)body.position[2]);
        KeplerOrbitLineDisplay display = go.AddComponent<KeplerOrbitLineDisplay>();
        display.body = body;
    }
    ```

### GravityCalculator.cs

- **Purpose**: Compute gravitational forces between celestial bodies.
- **Integration**:
  - Add `GravityCalculator.cs` to `Assets/Scripts/ScriptLibrary/Optional`.
  - Use in other scripts (e.g., `OrbitUpdater.cs`) to calculate forces:
    ```csharp
    Vector3d force = GravityCalculator.CalculateGravityForce(position, bodyPos, mass, body.mass);
    ```

### OrbitUpdater.cs

- **Purpose**: Update celestial body positions based on gravitational forces.
- **Integration**:
  - Add `OrbitUpdater.cs` to `Assets/Scripts/ScriptLibrary/Optional`.
  - Attach to each celestial body’s GameObject and assign other bodies:
    ```csharp
    foreach (var body in celestialBodies) {
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        body.transform = go.transform;
        body.transform.position = new Vector3((float)body.position[0], (float)body.position[1], (float)body.position[2]);
        OrbitUpdater updater = go.AddComponent<OrbitUpdater>();
        updater.otherBodies = celestialBodies;
        updater.mass = body.mass;
        updater.velocity = new Vector3d(body.velocity[0], body.velocity[1], body.velocity[2]);
    }
    ```

### NBodySimulator.cs

- **Purpose**: Simulate N-body gravitational interactions for dynamic orbits.
- **Integration**:
  - Add `NBodySimulator.cs` to `Assets/Scripts/ScriptLibrary/Optional`.
  - Attach to a GameObject and assign celestial bodies:
    ```csharp
    void Start() {
        NBodySimulator simulator = gameObject.AddComponent<NBodySimulator>();
        simulator.bodies = celestialBodies;
    }
    ```

### SpaceshipController.cs

- **Purpose**: Provide third-person spaceship navigation.
- **Integration**:
  - Add `SpaceshipController.cs` to `Assets/Scripts/ScriptLibrary/Optional`.
  - Attach to the player GameObject:
    ```csharp
    SpaceshipController controller = playerTransform.gameObject.AddComponent<SpaceshipController>();
    controller.nearestBody = celestialBodies.Find(b => b.name == "Sun"); // Optional gravity
    ```

### OrbitPathRenderer.cs

- **Purpose**: Render orbital paths to reduce visual clutter.
- **Integration**:
  - Add `OrbitPathRenderer.cs` to `Assets/Scripts/ScriptLibrary/Optional`.
  - Attach to each celestial body’s GameObject:
    ```csharp
    foreach (var body in celestialBodies) {
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        body.transform = go.transform;
        body.transform.position = new Vector3((float)body.position[0], (float)body.position[1], (float)body.position[2]);
        OrbitPathRenderer renderer = go.AddComponent<OrbitPathRenderer>();
        renderer.body = body;
    }
    ```

## Sample Code

### Adding Orbital Paths

```csharp
public class OrbitalPathRenderer : MonoBehaviour {
    public CelestialBody body;
    private LineRenderer lineRenderer;

    void Start() {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startColor = new Color(1, 1, 1, 0.2f);
        lineRenderer.endColor = new Color(1, 1, 1, 0.2f);
        lineRenderer.positionCount = 100;
        for (int i = 0; i < 100; i++) {
            lineRenderer.SetPosition(i, body.transform.position);
        }
    }
}
```

## Usage Notes

- **Performance**: The core library and optional scripts are lightweight, ensuring GTX laptop compatibility.
- **Precision**: Core double-precision and floating origin handle 50 AU distances. `NBodySimulator.cs` uses doubles but lacks advanced integrators.
- **Clutter**: `OrbitPathRenderer.cs` and label caps (~8–10) reduce visual clutter.
- **Usability**: `SpaceshipController.cs` and `HUDReadout.cs` enhance guidance. Extend with minimap and directional arrows (3–5).
- **JSON Accuracy**: Verify JSON data against NASA sources. `NBodySimulator.cs` is adaptable to NASA data.

## Limitations

- **Rendering**: Hybrid scaling is implemented, but sub-pixel rendering requires additional shaders.
- **UI**: Full HUD/minimap and label caps are not included.
- **ECS/DOTS**: Uses MonoBehaviour; consider ECS for galaxy-scale performance.
- **DLL Compatibility**: Ensure `PhysicsPlugin.dll` is compiled for Windows 64-bit.
- **Simplified Dynamics**: `NBodySimulator.cs` uses basic integration, less robust than `PhysicsPlugin.dll`.

## Next Steps

- Implement HUD/minimap with monochromatic design.
- Add sub-pixel rendering via shaders.
- Explore ECS/DOTS for large-scale simulations.
- Test JSON data with NASA-verified values.
- Enhance `NBodySimulator.cs` for JSON-driven NASA data.
