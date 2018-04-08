# AeonsTorn
A 2D Card TRPG Game

Written by James

## A simple Game Description

### Keywords
* 2D
* cards
* Dungeon-lite
* Story-based
* POV

### Target Player
* Tactical RPG lovers
* story explorers
* dungeon-like game player

### Inspiration: 
* Into the Breaches
* Slay the Spire
* Fire Emblem
* 戦場のヴァルキュリア
* 月圆之夜
* the Binding of Issac;

### Brief
Player starts with a basic deck of cards and basic ability, by collecting stronger cards and powerful items after battles, then finally challenge the boss.

### Features: 
* Dungeon-lite: every game player experience different combo of cards.
* Upgradable: unlock-able items and cards when player play a character for several times
* Chapter: player unlock character by passing several chapters.
etc.

## Characters
* “枪匠” ：武器大师，可以让手中的物质塑造成不同的形状。高加索人种，具体人物形象参考
* “吟游诗人”：使用鲁特琴，操纵音律。
- [ ] todo (4)
* “剧作家”：幕后黑手，心灵术士。把上述所有人全部关在塔里的罪魁祸首。

## Game Story
讲述方式：章节制
多人物视角的一个大故事（POV）
剧情
- [ ] todo


## Game Flow
每章节分为六个小战斗场景，一个boss战斗场景，三个情节支点；
情节支点决定剧情走向和boss
具体流程 
- [ ] todo


## Game World
- [ ] todo


## Game Experience
- [ ] todo

## Gameplay Mechanic
回合制游戏
游戏机制分为战棋和卡牌两部分。
### 战棋部分
战斗场地为 长\*宽 的棋盘，每个格子默认为可以在上面移动，有特殊地形

战斗开始时，随机分布一定敌人棋子在敌人生成区域，将玩家放在棋盘最底下的正中央

回合制，一定由玩家先开始游戏回合

玩家回合分为开始阶段，摸牌阶段，主阶段，结束阶段

1. 开始阶段：清算玩家身上的buff/debuff（中毒/多摸牌/流血）
2. 摸牌阶段： 摸牌 吟游诗人在摸牌的时候会为每一张卡牌附上音阶
3. 主阶段： 
    1. 自由移动/攻击：这个阶段可以自由移动 每次vertical移动消耗一点MP，TP与MP以1:敏捷值的换算方法兑换，每次攻击消耗一点TP，除了枪匠可以制造枪从而使攻击变成远程攻击，所有其他角色的攻击均为近战攻击
    2. 使用卡牌：大多数卡牌会消耗MP，如果所有卡牌都进入弃牌堆，则重新洗弃牌堆然后将其放置在卡组上
    3. 使用特性：一些英雄可以用上面的按钮来实现某种特殊效果，
        * 枪匠：造枪：消耗X张手牌进行一次制造，下回合开始阶段在三种可能的枪中选择一把获得
        * 吟游诗人：和弦：一回合一次，在使用了三张牌之后，若三张牌构成一个和弦，按上面的按钮则可以触发和弦的效果
4. 结束阶段：清算buff和其他一些玩意儿

在击败所有目标之后进入结算页面，结算打败敌人获得的金钱和卡牌（as STS）

在打败boss之后有重要剧情选项。

## 卡牌部分
玩家在游戏开始的时候拥有一个基础卡堆，通过打败敌人，幕间和剧情进展来获得新的卡牌

所有卡牌要不是普通类别要不属于某个职业

卡牌分为铁，银，金，密稀；强度和获得的难度均有不同

每张卡牌都拥有TP Cost 吟游诗人的卡牌则拥有音阶

### 卡牌描述(有且不限于)
* 对敌人/自己 造成伤害 施加 永久/非永久 buff/debuff（迅捷步伐/all：增加一点敏捷）
* 制造召唤物 或者魅惑敌人，他们变成你的队友
* 对一些地面制造区域效果（减速带，燃烧带）
* 快速移动（绕到某个敌人身后，往后退两格）
* 对于其他卡牌的改动（换弦/吟游诗人：随机改变所有其他卡牌的音阶，抽一张牌）
* 召唤青眼白龙（这是开玩笑的）
* 直接获得游戏胜利（当你收集起艾克佐迪亚的各个套装时）

## 道具
玩家可以在剧情中或者宝箱里获得道具

有些道具会影响剧情发展

有些则会提供增益

## 玩家基础数据
* 生命值
* 初始TP值
* 敏捷
* 每回合抽牌数
* 基础卡组
* 道具


## TODO
### For writer
- [ ] 游戏世界观
- [ ] 人物性格 造型 三观
- [ ] 游戏剧情

### For design 数值
- [ ] 卡牌设计
- [ ] 人物基础数据
- [ ] 敌人和敌人Behavior AI
- [ ] lots of things…
