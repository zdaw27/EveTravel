 EveTravel Portfolio
==========================
<a name="top"></a>
Unity version : 2019.4.17.f1

포트폴리오 설명: 
데스티니차일드의 밤의세계에서 플레이 할 수 있는 [이브의 모험] 모작하였습니다.

기능별 이미지 아래에 설명과 관련 스크립트를 하이퍼링크로 첨부하였습니다.
<p>
<p>
  <p>
    <p>
<hr/>

## 원작.

![Eve_original](https://user-images.githubusercontent.com/51247612/109377765-c4048400-7910-11eb-8cb0-3391fe3fc96a.gif)

## 모작.

![Eve_Mine](https://user-images.githubusercontent.com/51247612/109382834-1e5c0f80-7926-11eb-91a4-4c4e1337f991.gif)

<hr/>
<hr/>

## 맵 에디터.


![EditingMap](https://user-images.githubusercontent.com/51247612/109381449-e355de00-791d-11eb-9fce-bfa4ccc34b34.gif)

타일맵생성에 효율성을 더하고자 유니티 EditorWindow를 상속하여 맵에디터를 구현하였습니다.  
Serialize 가능한 HashSet, Dictionary 컨테이너를 사용.

### 관련 스크립트:  

맵 에디터:  
https://github.com/zdaw27/EveTravel/blob/main/Assets/Scripts/Editor/EveMapEditor.cs  

에디터데이터 저장용 스크립터블오브젝트:  
https://github.com/zdaw27/EveTravel/blob/main/Assets/Scripts/Editor/ScriptableObjects/EditorData.cs  

런타임에 사용하는 맵:  
https://github.com/zdaw27/EveTravel/blob/main/Assets/Scripts/EveMap.cs  

Serializable Containers:  
https://github.com/zdaw27/EveTravel/blob/main/Assets/Scripts/SerializableContainer/SerializableDictionary.cs  
https://github.com/zdaw27/EveTravel/blob/main/Assets/Scripts/SerializableContainer/SerializableHashSet.cs  


<hr/>
<hr/>

## 이펙트 매니저.


![EffectTest](https://user-images.githubusercontent.com/51247612/109385071-7e59b280-7934-11eb-8e51-1c3190796947.gif)

전역에서 어디서든 사용가능 하도록 제작한 EffectManager 입니다.  
내부적으로 ObjectPooling으로 관리가 되어있습니다.  
BaseEffect 클래스를 상속하여 새로운 이팩트를 확장할 수 있습니다.  

### 관련 스크립트:  

이펙트 매니저:  
https://github.com/zdaw27/EveTravel/blob/main/Assets/Scripts/EffectManager.cs  

오브젝트 풀:  
https://github.com/zdaw27/EveTravel/blob/main/Assets/Scripts/Misc/ObjectPool.cs  

EffectManager 에서 사용하는 BaseEffect:  
https://github.com/zdaw27/EveTravel/blob/main/Assets/Scripts/Effects/BaseEffect.cs  

BaseEffect 상속하여 다형성으로 실행되는 Effect 클래스들:  
https://github.com/zdaw27/EveTravel/blob/main/Assets/Scripts/Effects/DamageNumberEffect.cs  
https://github.com/zdaw27/EveTravel/blob/main/Assets/Scripts/Effects/LevelUpTextEffect.cs  
https://github.com/zdaw27/EveTravel/blob/main/Assets/Scripts/Effects/LootingEffect.cs  
https://github.com/zdaw27/EveTravel/blob/main/Assets/Scripts/Effects/ParticleEffect.cs  
https://github.com/zdaw27/EveTravel/blob/main/Assets/Scripts/Effects/PermanentEffect.cs  

이펙트 테스트 클래스:  
https://github.com/zdaw27/EveTravel/blob/main/Assets/Scripts/EffectTest.cs  



<hr/>
<hr/>

## 게임 매니저.


![GameManager](https://user-images.githubusercontent.com/51247612/109386433-e365d600-793d-11eb-863b-3062721811ba.png)

게임 전체 흐름을 제어하는 클래스 입니다.  
Finite State Machine 패턴을 사용하여 관리합니다.  

### 관련 스크립트:  

게임 매니저:  
https://github.com/zdaw27/EveTravel/blob/main/Assets/Scripts/GameManager.cs  

FSM:  
https://github.com/zdaw27/EveTravel/blob/main/Assets/Scripts/FSM/FSM.cs  

FSM 에서 제어하는 State 클래스들:  
https://github.com/zdaw27/EveTravel/blob/main/Assets/Scripts/FSM/State/IState.cs  
https://github.com/zdaw27/EveTravel/blob/main/Assets/Scripts/FSM/State/GameManger/BattleState.cs  
https://github.com/zdaw27/EveTravel/blob/main/Assets/Scripts/FSM/State/GameManger/GameOverState.cs  
https://github.com/zdaw27/EveTravel/blob/main/Assets/Scripts/FSM/State/GameManger/InputState.cs  
https://github.com/zdaw27/EveTravel/blob/main/Assets/Scripts/FSM/State/GameManger/IntroState.cs  
https://github.com/zdaw27/EveTravel/blob/main/Assets/Scripts/FSM/State/GameManger/LevelUpState.cs  
https://github.com/zdaw27/EveTravel/blob/main/Assets/Scripts/FSM/State/GameManger/MapChangeState.cs  
https://github.com/zdaw27/EveTravel/blob/main/Assets/Scripts/FSM/State/GameManger/PathFindState.cs  
https://github.com/zdaw27/EveTravel/blob/main/Assets/Scripts/FSM/State/GameManger/ReadyState.cs  


<hr/>
<hr/>

## 캐릭터.


![image](https://user-images.githubusercontent.com/51247612/109386676-9d117680-793f-11eb-881b-83d9a5a59579.png)

전투에 가담하는 캐릭터 클래스 입니다.  
Player 와 Enemy 클래스는 Chracter 클래스를 상속합니다.  
추후에 또다른 캐릭터를 확장할 때 Character 클래스를 상속하여 구현합니다.  

### 관련 스크립트:  

추상 캐릭터 클래스:  
https://github.com/zdaw27/EveTravel/blob/main/Assets/Scripts/Character/Character.cs  

Player:  
https://github.com/zdaw27/EveTravel/blob/main/Assets/Scripts/Character/Player.cs  

Enemy:  
https://github.com/zdaw27/EveTravel/blob/main/Assets/Scripts/Character/Enemy.cs  


<hr/>
<hr/>

## 구조 디자인.

### 이벤트 객체.  
![image](https://user-images.githubusercontent.com/51247612/109387504-27100e00-7945-11eb-92cf-a45bf8bc42a2.png)  

### Setter 콤포넌트.  
![image](https://user-images.githubusercontent.com/51247612/109387655-faa8c180-7945-11eb-9f2e-3e066a8230b3.png)  

### listener 콤포넌트.  
![image](https://user-images.githubusercontent.com/51247612/109387609-b9181680-7945-11eb-8e5d-d1066e544720.png)  


추후에 확장에 유연하도록, 최대한 클래스끼리 엮이지 않고 독립적인 개발이 가능하도록 노력했습니다.  
모든 MonoBehaviour 클래스들 Scene이 종료되면 모두 삭제되고, Scene Load 시 클린 상태를 유지하여 다른 클래스의 동작방식을 이해하지 않고도 개발이 가능하게 유지했습니다.  
Unite 2017 에서 ScriptableObject 를 이용해 구조 설계하는 것에 영감받아 Singltone 을 이용하지 않고 데이터 흐름을 제어해봤습니다.  
런타임 데이터를 담고있는 GameData 클래스는 스스로 데이터 인풋과 아웃풋을 가담하지 않고 외부 Setter 컴포넌트가 그 기능을 담당합니다.  
모든 UI 클래스는 이벤트에 기반하여 동작하도록 설계하였으며, 독립적인 개발이 가능합니다.  
각 클래스의 고유 기능은 그 기능에 집중하기 위해서 이벤트 리스닝, Subscribe 기능, 종속성 주입 기능은 전부 외부 콤포넌트가 담당합니다.  
프리팹 단위로 오브젝트를 관리하였으며, 단순 프리팹 드래그앤 드랍 으로 개발 테스트가 용이하도록 집중했습니다.  


### 관련 스크립트:  

추상 캐릭터 클래스:  
https://github.com/zdaw27/EveTravel/blob/main/Assets/Scripts/Character/Character.cs  

Player:  
https://github.com/zdaw27/EveTravel/blob/main/Assets/Scripts/Character/Player.cs  

Enemy:  
https://github.com/zdaw27/EveTravel/blob/main/Assets/Scripts/Character/Enemy.cs  






<a href="#top">123</a>
