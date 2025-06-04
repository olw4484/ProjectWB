# ⚔️ 웨어베어 vs 다크엘프

5일간 진행된 개인 프로젝트로, 마비노기 영웅전의 보스 러쉬 구조와 소울라이크 전투 시스템에서 영감을 받아 설계된 1:1 액션 게임입니다.

플레이어는 활을 주 무기로 사용하는 다크엘프가 되어  
거대한 괴수형 보스인 웨어베어와 긴장감 넘치는 일기토를 벌이게 됩니다.  
패턴 기반 보스 AI, URP 연출, 회피/패링 중심의 리듬 전투에 중점을 두었습니다.

**⚠️ 현재는 프로토타입/미완성 상태입니다.**
- 일부 애니메이션, 보스 패턴, UI, 이펙트 등은 추후 업데이트 예정입니다.

---

# ⚔️ Werebear vs Dark Elf

A 5-day prototype of a boss-rush style action game inspired by *Vindictus* and *Soulslike* combat systems.

You play as a dark elf archer facing off against a monstrous werebear in a one-on-one battle of reflexes, timing, and survival.  
This project focuses on tight combat loops, state-driven AI, and cinematic URP effects — all within a confined boss arena.

---

## 🎯 Features (Prototype/Incomplete)
- [x] Phase-based boss AI (3 phases with evolving attack chains)
- [x] Bow-based combat (Monster Hunter style aim/draw/fire loop)
- [x] Cinematic camera, URP post-processing (DoF, Vignette, Chromatic Aberration)
- [x] Basic dodge, roll, and limited parry actions
- [ ] UI/UX polish (incomplete)
- [ ] Complete animation blend trees (WIP)
- [ ] Additional boss patterns, hit feedback (planned)

---

## 🛠 How to Run

- **Unity Version:** 2022.3.62f1
- Open the project in Unity, set your platform to Windows, and build (see `/Assets/Scenes/MainScene.unity`)
- Controls: WASD (move), Mouse (aim), Left Click (fire), Space (dodge), Right Click (aim)

---

## 📦 Asset Credits

- **Werebear**: [Asset Store link](https://assetstore.unity.com/packages/3d/characters/animals/mammals/werebear-150288)
- **Death Archer Girl (Morgana)**: [Asset Store link](https://assetstore.unity.com/packages/3d/characters/humanoids/fantasy/death-archer-girl-morgana-294209)
- All code/design: © 김민수 (solo project, 2025)

---

## ⚠️ Known Issues & Limitations

- 일부 기능 및 애니메이션 미구현
- 플레이 도중 에러/예기치 못한 종료 가능성 있음
- 모든 기능은 포트폴리오/교육용 시연 목적

---

Made as a solo 5-day project.  
All animation logic, gameplay code, and design structure written in C# with Unity.
