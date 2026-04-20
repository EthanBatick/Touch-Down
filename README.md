This is my first full Unity Game about landing a rocket booster. I've provided the project here for people to develop further on as well as different builds you may download to play. Thanks for taking the time to check this out!


# 🚀 TouchDown – Falcon 9 Landing Game

A 2D physics-based rocket landing simulator built in Unity, inspired by real Falcon 9 booster landings.

---

## 🎮 Overview

TouchDown challenges players to control a rocket and land it safely using realistic physics constraints.

You must manage:

* Throttle
* Rotation
* Velocity
* Fuel

A successful landing requires:

* Low vertical speed
* Low horizontal drift
* Minimal tilt angle
* Landing on a valid landing pad

Otherwise… 💥

---

## 🕹 Controls

| Key   | Action                               |
| ----- | ------------------------------------ |
| W     | Increase throttle                    |
| S     | Decrease throttle                    |
| A     | Rotate left                          |
| D     | Rotate right                         |
| Space | Toggle landing legs (if implemented) |

---

## 🧠 Gameplay Mechanics

* Real-time physics using Unity 2D Rigidbody
* Throttle-based thrust system
* Gimbaled engine plume (visual feedback)
* Explosion system on crash
* Landing validation system (speed + angle + pad detection)

---

## 🌍 Web Build

The game runs in-browser using Unity WebGL.

### Features:

* Fullscreen scaling
* Responsive canvas resizing
* Auto fullscreen attempt
* Smooth plume particle system

---

## 🖥 Running Locally

### 1. Navigate to build folder

cd TouchDownBuild

### 2. Start server

python -m http.server 8000

### 3. Open in browser

http://localhost:8000

---

## 🌐 Hosting (Optional)

You can expose your game to the internet using:

* Cloudflare Tunnel
* ngrok

Example:

cloudflared tunnel run

---

## 📁 Project Structure

/Build                → Unity WebGL build files
/TemplateData         → Unity UI assets
index.html            → Main entry point
style.css             → Fullscreen styling

---

## ⚙️ Key Systems

### Landing System

* Trigger collider checks landing validity
* Evaluates:

  * Vertical velocity
  * Horizontal velocity
  * Rocket tilt
* Loads next scene or restarts

---

### Explosion System

* Breaks rocket into pieces
* Spawns fire particles
* Physics-based debris

---

### Plume System

* Spawns particles based on throttle
* Gimbal adjusts direction dynamically
* Frame-rate independent spawning

---

## 🔧 Tech Stack

* Unity (2D, WebGL)
* C#
* HTML / CSS / JS

---

## 🚀 Future Ideas

* Multiple levels
* Moving landing pads
* Wind simulation
* Scoring system
* Mobile controls
* GPU-based particle system

---

## 🧑‍💻 Author

Ethan Batick
Syracuse University – Computer Science

---

## ⚠️ Notes

* Requires a local server (won’t run via file://)
* Fullscreen may require user interaction depending on browser
