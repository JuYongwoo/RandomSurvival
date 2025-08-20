![게임 스크린샷](https://github.com/JuYongWoo/RandomSurvival/blob/main/Images/ScreenShot.png)


# 프로젝트 설명
- 현재 개발 중인 게임입니다.
- 게임은 탑다운 뷰 형식으로 진행됩니다.
- 이동 로직은 RTS 게임과 같도록 하였기 때문에 마우스 만으로도 플레이 가능합니다.
- 30분 간 살아남는 것이 게임 승리 목표입니다.


## 사용 기술 및 설계 원칙

### **SO를 통한 플레이어 변수 조작**
- **ScriptableObject(SO)** 를 기반으로 한 플레이어 변수 설정
- 인스펙터 창에서 직관적으로 이벤트 정의 가능
- 유지보수 용이 & 디자이너와 협업 최적화

---

### **.csv파일을 통한 유연한 맵 수정과 로드**
- **csv** 파일에 레이블을 입력
- 게임이 시작될 때 csv파일을 로드하고 레이블에 따라 맵을 자동 제작
- 유지보수가 매우 용이하여 간단한 수정으로 기획적 레벨 디자인이 가능

---

### **UI 버튼 이벤트 처리 자동화**
- Util 클래스 내 템플릿을 이용한 함수로 **enum ↔ GameObject** 딕셔너리 매핑
- GameObject 이름 참조 X → **enum 기반 참조로 안정성 강화**
- 오타나 참조 오류 발생률 감소

---

### **FSM 패턴을 이용한 플레이어 액션 수행**
- Idle-Attack-Move 등으로 이루어진 State Enum을 사용
- RTS와 같은 공격, 이동, 공격+이동 등의 다양한 움직임을 순차적으로 해결

---

## 스크립트 구조

```
├── Enemy                    # EnemyBase를 상속받는 적 스크립트
│   ├── EnemyBase
│   ├── Gate
│   └── Wolf
│
├── Item                      # 게임 진행을 위한 여러가지 아이템 스크립트
│   ├── Coin
│   ├── Spawner
│   └── Teleport
│
├── Managers                  # 게임 구성요소 처리를 위한 매니저들
│   ├── AudioManager
│   ├── ComponentUtility
│   └── ManagerObject
│
├── Player                    # 플레이어 변수 관리와 FSM 패턴을 이용한 구현과 관련된 스크립트
│   ├── CameraMove
│   ├── Player
│   ├── PlayerData
│   ├── PlayerFace
│   ├── PlayerStateMachine
│   └── PlayerStatObject
│
├── Scenes                     # 각 씬마다 처리할 고유 이벤트를 위한 스크립트
│   ├── MainSceneObject
│   ├── LoseSceneManager
│   └── WinSceneManager
│
├── UI                     # 역할에 따라 나눈 UI의 패널 or 캔버스에 붙은 스크립트
│   ├── CountPanel
│   ├── HowToPlay
│   ├── PortraitPanel
│   ├── StatPanel
│   ├── TimePanel
│   └── TitleCanvas

```

---

