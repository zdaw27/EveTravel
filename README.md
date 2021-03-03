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
<hr/>

## 목차.
<a href="#APK">APK 다운로드 링크.</a>  
<a href="#original">원작 모작 비교.</a>  
<a href="#diagram">클래스 다이어그램</a>  
<a href="#map editor">맵 에디터</a>  
<a href="#effect manager">이펙트 매니저</a>  
<a href="#game manager">게임 매니저</a>  
<a href="#character">캐릭터</a>  
<a href="#architecture design">구조 디자인</a>  
<a href="#update">추후 업데이트 내용 </a>  

<hr/>
<hr/>


<a name="APK"></a>
## APK 다운로드 링크.  
https://drive.google.com/file/d/11PsL1OhmjAasNvL3-S9TVjXD9vVYx4wa/view?usp=sharing


<hr/>
<hr/>

<a name="original"></a>
## 원작.

![Eve_original](https://user-images.githubusercontent.com/51247612/109377765-c4048400-7910-11eb-8cb0-3391fe3fc96a.gif)

## 모작.

![Eve_Mine](https://user-images.githubusercontent.com/51247612/109382834-1e5c0f80-7926-11eb-91a4-4c4e1337f991.gif)

<hr/>
<hr/>

<a name="diagram"></a>
## 클래스 다이어그램.


![diagram](https://user-images.githubusercontent.com/51247612/109434764-2917c100-7a5a-11eb-948a-2a1ccff2b92c.png)

주요 클래스들의 포함 및 상속관계를 그려봤습니다.

<hr/>
<hr/>


<a name="map editor"></a>
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

<a name="effect manager"></a>
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

<a name="game manager"></a>
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

<a name="character"></a>
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

<a name="architecture design"></a>
## 구조 디자인.

### 이벤트 객체.  
![image](https://user-images.githubusercontent.com/51247612/109387504-27100e00-7945-11eb-92cf-a45bf8bc42a2.png)  

### Setter 콤포넌트.  
![image](https://user-images.githubusercontent.com/51247612/109387655-faa8c180-7945-11eb-9f2e-3e066a8230b3.png)  

### listener 콤포넌트.  
![image](https://user-images.githubusercontent.com/51247612/109387609-b9181680-7945-11eb-8e5d-d1066e544720.png)  


추후에 확장에 유연하도록, 최대한 클래스끼리 엮이지 않고 독립적인 개발이 가능하도록 노력했습니다.  
모든 MonoBehaviour 클래스들 Scene이 종료되면 모두 삭제되고, Scene Load 시 클린 상태를 유지하여 제거되지않고 살아남은 클래스에 존재로 인한 복잡성을 없앴습니다.  
Unite 2017 에서 ScriptableObject 를 이용해 구조 설계하는 것에 영감받아 Singleton 을 이용하지 않고 데이터 흐름을 제어해봤습니다.  
런타임 데이터를 담고있는 GameData 클래스는 스스로 데이터 인풋과 아웃풋을 가담하지 않고 외부 Setter 컴포넌트가 그 기능을 담당합니다.  
모든 UI 클래스는 이벤트에 기반하여 동작하도록 설계하였으며, 독립적인 개발이 가능합니다.  
각 클래스의 고유 기능은 그 기능에 집중하기 위해서 이벤트 리스닝, 종속성 주입 기능은 최대한 외부 콤포넌트가 담당하도록 했습니다.  
프리팹 단위로 오브젝트를 관리하였으며, 단순 프리팹 드래그앤 드랍으로 개발 테스트가 용이하도록 집중했습니다.  


### 관련 스크립트:  

GameData:  
https://github.com/zdaw27/EveTravel/blob/main/Assets/Scripts/ScriptableObjects/Runtime/GameData.cs  

Setter:  
https://github.com/zdaw27/EveTravel/blob/main/Assets/Scripts/Setter/EnemySetter.cs  
https://github.com/zdaw27/EveTravel/blob/main/Assets/Scripts/Setter/EveMapSetter.cs  
https://github.com/zdaw27/EveTravel/blob/main/Assets/Scripts/Setter/PlayerSetter.cs  

Event:  
https://github.com/zdaw27/EveTravel/blob/main/Assets/Scripts/ScriptableObjects/GameEvent.cs  

Event Listner:  
https://github.com/zdaw27/EveTravel/blob/main/Assets/Scripts/EventListenerComponent.cs  

UI Script 폴더:  
https://github.com/zdaw27/EveTravel/tree/main/Assets/Scripts/UIScripts  


<hr/>
<hr/>

<a name="update"></a>
## 추후 업데이트 내용.

1 - 몬스터 및 타일 풀링 시스템.  
2 - DataDriven 기반 아키텍처 구성:  
Item 능력치와, Monster 능력치등 기타 데이터들을 파일 단위로 관리하여 게임을 업데이트 할 수 있도록 하기 위함.
3 - 맵 랜덤 생성 기능:   
맵을 부분별로 분할하여 프리팹을 저장하고 랜덤조합으로 생성.   



<a href="#top"></a>
