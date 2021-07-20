# Laser Chess 3D in Unity
몰입캠프 Week 3 과제는 자유구현이었다. 'Laser Chess 3D in Unity'는 <<더지니어스>>에서 소개된 '레이저 장기'와 기존의 Laser Chess 보드게임을 Modify한 3D 가상공간 보드게임으로, 아래의 exe 파일을 다운받으면 직접 게임을 Play할 수 있다.   

## Team information
##### Members : 장종원, 임가은    

## 게임 규칙
### 경기 준비
Laser Chess는 가로 8개와 세로 8개, 총 64개의 정사각형으로 이루어진 체스보드에서 진행된다. 각 선수는 **5가지 종류의 기물**을 가지고 시작한다. 
|기물|설명|
|----------|--------------------------------------------------------------------------------------------------------|
|Laser|레이저 빔이 발사되어 상대편을 공격한다. 레이저 빔은 경기장내부 방향으로만 발사할 수 있다. Laser는 공격받을 수 없다.|
|King|4개의 면 모두 공격받을 수 있다. King이 죽으면 게임에서 패배한다.|
|TriKnight|1개의 면이 거울이다. 나머지 2개의 면은 공격받을 수 있다.|
|RectKnight|1개의 면이 거울이다. 나머지 3개의 면은 공격받을 수 있다.|
|Splitter|중심부에 있는 1개의 면이 양면 거울이자 투과판이다. 입사한 레이저를 반사, 및 동시에 투과시켜 2갈래로 나눈다. Splliter는 공격받아도 죽지 않는다.|

![pieces2](https://user-images.githubusercontent.com/59522019/126342210-4b32f731-2fc7-4ef9-ad95-08459f22d4a3.JPG)


#### 경기 진행

![KakaoTalk_20210720_232830161](https://user-images.githubusercontent.com/59522019/126342380-e4551d54-798d-4bbe-abb8-a4d68be67e51.gif)

게임이 시작되면, 기물들은 다음 그림과 같은 포지션으로 배치된다. 각 선수는 Laser 1개, King 1개, TriKnight 4개, RectKnight2개, Splitter 1개씩 총 9개의 기물을 가지고 시작한다. 하나의 말은 한 칸에 들어간다. 파란색 기물을 사용하는 선수를 "Blue"라고 하며 빨간색 기물을 사용하는 선수를 "Red"라고 한다. 언제나 Blue가 첫 수를 두게 되며 이후에는 Blue와 Red가 번갈아가며 한 수 씩 둔다. 자신의 차례가 되면 반드시 기물을 움직여야 한다. 

기물을 움직이는 방법에는 크게 2가지(총 6가지의 가능한 경우의 수)가 있다. 
```c 
1. 각도 조정 : 왼쪽, 또는 오른쪽으로 90도 돌릴 수 있다.
2. 위치 조정 : 상, 하, 좌, 우 중 한 칸을 움직일 수 있다.
```

기물을 움직인 이후에는 자신의 **레이저가 1번 발사**된다. 레이저에 맞은 기물은 레이저를 반사하거나, 죽는다. 만약 레이저에 맞은 면이 거울면일 경우, 레이저가 반사된다(Splitter의 경우, 중심부의 거울면이 레이저를 반사하는 동시에 입사 방향으로 투과되어 레이저가 2 갈래로 나뉜다. 거울이 아닌 면에 레이저를 맞은 기물은 상대편의 것이든, 내 편의 것이든 죽는다.  

#### 승리 조건
상대편 진영의 King을 부서트리면 승리한다. 
 

## Environment
#### 개발 환경 : Unity 2020.3.14f1 Personal  


## Download our game!
https://drive.google.com/drive/folders/1FKoiz0WkiLnDTyhHP-RKd-eRvV6ER-cR?usp=sharing
