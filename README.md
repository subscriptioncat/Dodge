# Dodge
팀 홈페이지 - https://www.notion.so/07-55b9e1e5815747a790927d14b434bfa6    

>- 팀 명 : 07 -칠면조      
>- 프로젝트 명 : 땃쥐는 오늘도 닷지     
>- 프로젝트 소개 : 곰플레이어의 닷지 게임을 모티브로 만든 게임입니다.     
>- 팀장 : 김도현 - BulletData.cs, BulletController.cs, EnemtyBullet.cs, TopDownShooting.cs, 몬스터 패턴     
>- 팀원 : 배인호 - RankingManager.cs, SelectManager.cs, StartManager.cs, UIManager.cs, MonsterData.cs, Utils폴더 EndCondition파일 및 MonsterPattern을 제외한 모든 파일들, EndingScene제외 모든 UI들    
>- 팀원 : 채이환 - SoundManager.cs, DataManager.cs, MonsterController.cs, CharacterAudio, BGMPlayer, MonsterPattern.cs,         
>- 팀원 : 손병의 - PlayerInputController.cs, TopDownCharacterController.cs, TopDownAimRotation.cs, TopDownMovement.cs, RankingManager.cs         
>- 팀원 : 곽민규 - BaseCharacter.cs, Item.cs, PlayerData.cs   
       
StartScene씬에서 Space Bar 누르면 SelectScene씬으로 전환       
SelectScene씬에서 F1(Player1)누르면 Player1캐릭터 선택할 수 있으며 F2(Player2)를 누르면 Player2커서가 활성화 되면서 캐릭터 선택이 가능합니다.     
커서는 1P W,A,S,D로 조작가능하며 2P는 방향키로 조작이 가능합니다.     
스페이스바로 캐릭터를 선택시 MainScene으로 전환되며 게임이 시작됩니다.     
게임에서 화면밖으로 이동이 불가하며 총알을 맞거나 적이랑 부딪치면 체력이 감소합니다.     
만약 체력이 0이 된다면 게임이 끝나고 EndingScene으로 전환되며 점수가 표시됩니다.      
    
