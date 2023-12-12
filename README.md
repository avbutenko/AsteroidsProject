# Intro
Its is a simple example of [asteroids game](https://en.wikipedia.org/wiki/Asteroids_(video_game)). Idea of creating such a game originally came from [this video](https://www.youtube.com/watch?v=syvjmS-mflY&t=1249s) where author was implementing project according to [those requirements](https://docs.google.com/document/d/1_2FPU2l_L7s5MPc9GJwN6QKcRNTRWgqKJasJ07PMXbY/edit#heading=h.92by3zo281gq). So please find below my own vision of how it could be done.

# GamePlay
[Here](https://youtu.be/EyfP4JsrVl0) you can find a live gameplay demo.

# ScreenShots
<p align="center">
  <img width="600" src="docs/AsteroidsProject_MainMenuScreen.png" alt="Gameplay">
  <img width="600" src="docs/AsteroidsProject_Gameplay01.png" alt="Gameplay">
  <img width="600" src="docs/AsteroidsProject_Gameplay02.png" alt="Gameplay">
  <img width="600" src="docs/AsteroidsProject_Gameplay03.png" alt="Gameplay">
  <img width="600" src="docs/AsteroidsProject_GamePauseScreen.png" alt="Gameplay">
  <img width="600" src="docs/AsteroidsProject_GameOverScreen.png" alt="Gameplay">
</p>

# Controls
- **W** - Accelerate
- **A** - Rotate Left
- **D** - Rotate Right
- **Left Click** - Primary Weapon Attack
- **Right Click** - Secondary Weapon Attack
- **Esc** - Pause

# Design desicions
There were no direct restrictions mentioned in requirements. Therefore I decided to use several third party open source frameworks which are in common use across community:
- [Zenject](https://github.com/modesttree/Zenject)
- [LeoEcsLite](https://github.com/Leopotam/ecslite)
  - [LeoEcsLite-Extendedsystems](https://github.com/Leopotam/ecslite-extendedsystems)
  - [LeoEcsLite-Unityeditor](https://github.com/Leopotam/ecslite-unityeditor)
  - [LeoEcsLite-Physics](https://github.com/supremestranger/leoecs-lite-physics)
- [UniTask](https://github.com/Cysharp/UniTask/tree/master)
- [UniRx](https://github.com/neuecc/UniRx/tree/master)
  
# Supported platforms
- PC
