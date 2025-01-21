using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager Instance;

    [SerializeField]
    private string defaultLanguage = "en";

    private string currentLanguage;
    private Dictionary<string, string> localizedTexts;
    private Dictionary<string, string> localizedCardTexts;

    private void Awake()
    {
        //GameManager.Instance.localizationManager = this;
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        currentLanguage = defaultLanguage;
        LoadLocalizationData();
    }

    private void LoadLocalizationData()
    {
        localizedTexts = new Dictionary<string, string>
        {
            // { "언어_Key", "Value" } Key == ItemData.itemNameKey

            //UI 텍스트//
            //상점
            { "en_ShopText1", "Are you going to buy this item? " },
            { "kr_ShopText1", "이 아이템을 구매하시겠습니까?" },
            { "jp_ShopText1", "このアイテムを購入しますか？" },
            { "en_ShopText2", "Yes" },
            { "kr_ShopText2", "네" },
            { "jp_ShopText2", "はい" },
            { "en_ShopText3", "No" },
            { "kr_ShopText3", "아니요" },
            { "jp_ShopText3", "いいえ" },
            { "en_ShopText4", "You don't have enough gold." },
            { "kr_ShopText4", "골드가 부족합니다." },
            { "jp_ShopText4", "ゴールドが足りません。" },
            { "en_ShopText5", "Close" },
            { "kr_ShopText5", "닫기" },
            { "jp_ShopText5", "閉じる" },

            //메인 로비
            { "en_ShopButton", "Go to Shop" },
            { "kr_ShopButton", "상점으로 이동" },
            { "jp_ShopButton", "ショップへ" },
            { "en_TrainingButton", "Go to Training" },
            { "kr_TrainingButton", "훈련장으로 이동" },
            { "jp_TrainingButton", "トレーニングへ" },
            { "en_PrepareButton", "Prepare for the Dungeon" },
            { "kr_PrepareButton", "던전입장 준비" },
            { "jp_PrepareButton", "ダンジョンへ" },
            { "en_InventoryButton", "Go to Inventory" },
            { "kr_InventoryButton", "인벤토리로 이동" },
            { "jp_InventoryButton", "インベントリへ" },
            { "en_OptionButton", "Option" },
            { "kr_OptionButton", "옵션" },
            { "jp_OptionButton", "オプション" },
            { "en_ExitButton", "Exit" },
            { "kr_ExitButton", "게임종료" },
            { "jp_ExitButton", "ゲーム終了" },

            //옵션
            { "en_OptionWindowToggle", "Window mode Toggle" },
            { "kr_OptionWindowToggle", "창모드 토글" },
            { "jp_OptionWindowToggle", "ウィンドウモードトグル" },
            { "en_OptionLanguage", "Language :" },
            { "kr_OptionLanguage", "언어 :" },
            { "jp_OptionLanguage", "言語 :" },
            { "en_OptionEndGame", "End Game" },
            { "kr_OptionEndGame", "게임종료" },
            { "jp_OptionEndGame", "ゲーム終了" },
            { "en_Resolution", "Resolution" },
            { "kr_Resolution", "해상도" },
            { "jp_Resolution", "解像度" },
           

            //아이템 텍스트//
            //id = 0
            { "en_Cheap_Potion", "Cheap Potion" },
            { "kr_Cheap_Potion", "싸구려 포션" },
            { "jp_Cheap_Potion", "安いポーション" },
            { "en_Cheap_Potion_Desc", "A cheap portion that restores 10% of HP." },
            { "kr_Cheap_Potion_Desc", "HP 10% 를 회복하는 싸구려 포션." },
            { "jp_Cheap_Potion_Desc", "HP 10% を回復する安いポーション"},
            //id = 1
            { "en_Normal_Potion", "Normal Potion" },
            { "kr_Normal_Potion", "평범한 포션" },
            { "jp_Normal_Potion", "通常ポーション" },
            { "en_Normal_Potion_Desc", "A normal portion that restores 25% of HP." },
            { "kr_Normal_Potion_Desc", "HP 25% 를 회복하는 평범한 포션." },
            { "jp_Normal_Potion_Desc", "HP 25% を回復する通常ポーション"},
            //id = 2
            { "en_Great_Potion", "Great Potion" },
            { "kr_Great_Potion", "훌륭한 포션" },
            { "jp_Great_Potion", "素晴らしいポーション" },
            { "en_Great_Potion_Desc", "A great portion that restores 50% of HP." },
            { "kr_Great_Potion_Desc", "HP 50% 를 회복하는 훌륭한 포션." },
            { "jp_Great_Potion_Desc", "HP 50% を回復する素晴らしいポーション"},
            //id = 3
            { "en_Elixir", "Elixir" },
            { "kr_Elixir", "엘릭서" },
            { "jp_Elixir", "エリクサー" },
            { "en_Elixir_Desc", "A powerful portion that restores 100% of HP." },
            { "kr_Elixir_Desc", "HP 100% 를 회복하는 강력한 포션." },
            { "jp_Elixir_Desc", "HP 100% を回復する強力なポーション"},
            //id = 4
            { "en_Mythic_Elixir", "Mythic Elixir" },
            { "kr_Mythic_Elixir", "신화의 엘릭서" },
            { "jp_Mythic_Elixir", "神話のエリクサー" },
            { "en_Mythic_Elixir_Desc", "Revives the dead with 50% of HP." },
            { "kr_Mythic_Elixir_Desc", "HP 50% 를 회복하며 죽은 자를 부활시키는 엘릭서." },
            { "jp_Mythic_Elixir_Desc", "HP 50% を回復し、死者を蘇らせるエリクサー"},

        };
        localizedCardTexts = new Dictionary<string, string>
        {
            //스킬 카드 (스킬카드의 경우, kr,jp,en순으로 텍스트 작성)
            //스킬 카드의 Effects의 개수 == params object[] args 에 들어가는 value의 개수. 
            //전사 스킬 리스트 0.찌르기 1.아크 검술 2.방패치기(shield attack) 3.하늘 가르기 4.영웅등장 5.일섬 6.오라 블레이드 7.자가회복 8.광역회복 9.날카로운 칼날 10.견고한 방패 11.방패들어!
            {"kr_Skill0","찌르기" },
            {"jp_Skill0","剣突き" },
            {"en_Skill0","Sword Stab" },
            {"kr_Skill0_Desc", "칼로 적을 찔러 {0}의 데미지를 입힙니다." },
            {"jp_Skill0_Desc", "剣で敵を突き刺し {0}ダメージを与えます。" },
            {"en_Skill0_Desc", "Stab the enemy with a sword to deal {0}damage" },

            {"kr_Skill1","아크 검술" },
            {"jp_Skill1","アーク剣術" },
            {"en_Skill1","Arc Swordsmanship" },
            {"kr_Skill1_Desc", "가문의 비전 검술로 적을 2회 공격하여 {0}의 데미지를 입힙니다." },
            {"jp_Skill1_Desc", "家門の秘伝剣術で敵を2回攻撃して {0}ダメージを与えます。" },
            {"en_Skill1_Desc", "Attack the enemy twice with the Secret swordsmanship of the house to deal {0}damage." },

            {"kr_Skill2","방패들어!" },
            {"jp_Skill2","シールドアップ！" },
            {"en_Skill2","Shield Up!" },
            {"kr_Skill2_Desc", "{0}초 동안 받는 데미지를 감소시킵니다." },
            {"jp_Skill2_Desc", "{0}秒間受けるダメージを減少させます。" },
            {"en_Skill2_Desc", "Reduces incoming damage for {0}seconds." },

            {"kr_Skill3","하늘 가르기" },
            {"jp_Skill3","スカイスラッシュ" },
            {"en_Skill3","Sky Slash" },
            {"kr_Skill3_Desc", "하늘을 가르는 검술로 적을 공격하여 {0}의 데미지를 입힙니다." },
            {"jp_Skill3_Desc", "空を裂く剣術で敵を攻撃して {0}ダメージを与えます。" },
            {"en_Skill3_Desc", "Attack the enemy with a sword that splits the sky to deal {0}damage." },

            {"kr_Skill4","영웅등장" },
            {"jp_Skill4","ヒーローアピアランス" },
            {"en_Skill4","Hero Appearance" },
            {"kr_Skill4_Desc", "높히 점프후 지면을 강력하게 강타하는 기술로 {0}의 데미지와 {1}초 동안 슬로우 부여." },
            {"jp_Skill4_Desc", "高くジャンプして地面を強く打つ技で {0}ダメージと {1}秒間スローを与えます。" },
            {"en_Skill4_Desc", "A technique that jumps high and strikes the ground powerfully to deal {0}damage and slow for {1}seconds." },

            {"kr_Skill5","일섬" },
            {"jp_Skill5","一刀両断" },
            {"en_Skill5","Single Stroke" },
            {"kr_Skill5_Desc", "적을 일격에 베어 {0}의 관통형 데미지를 입힙니다." },
            {"jp_Skill5_Desc", "敵を一撃で斬って {0}の貫通ダメージを与えます。" },
            {"en_Skill5_Desc", "Cut the enemy in one blow to deal {0}penetrating damage. " },

            {"kr_Skill6","오라 블레이드" },
            {"jp_Skill6","オーラブレード" },
            {"en_Skill6","Aura Blade" },
            {"kr_Skill6_Desc", "정면에 존재하는 모든적을 강력한 검기로 베어내어 {0}의 데미지를 입힙니다." },
            {"jp_Skill6_Desc", "正面に存在する全ての敵を強力な斬撃で斬り {0}ダメージを与えます。" },
            {"en_Skill6_Desc", "Cut all enemies in front with a powerful aura sword and deal {0}damage." },

            {"kr_Skill7","자가회복" },
            {"jp_Skill7","セルフヒーリング" },
            {"en_Skill7","Self Healing" },
            {"kr_Skill7_Desc", "자신의 체력을 {0}회복합니다." },
            {"jp_Skill7_Desc", "自分の体力を {0}回復します。" },
            {"en_Skill7_Desc", "Recover health {0}." },

            {"kr_Skill8","광역회복" },
            {"jp_Skill8","エリアヒーリング" },
            {"en_Skill8","Area Healing" },
            {"kr_Skill8_Desc1", "파티원 전체의 체력을" },
            {"kr_Skill8_Desc2", "회복합니다." },
            {"jp_Skill8_Desc1", "パーティ全体の体力を {0}回復します。" },
            {"en_Skill8_Desc1", "Recover the health of the entire party {0}." },

            {"kr_Skill9","날카로운 칼날" },
            {"jp_Skill9","シャープブレード" },
            {"en_Skill9","Sharpened Blade" },
            {"kr_Skill9_Desc", "{0} 초 동안 더욱 날카로운 공격 시전." },
            {"jp_Skill9_Desc", "{0} 秒間より鋭い攻撃を行います。" },
            {"en_Skill9_Desc", "Cast a sharper attack for {0}" },

            {"kr_Skill10","견고한 방패" },
            {"jp_Skill10","タフシールド" },
            {"en_Skill10","Tough Shield" },
            {"kr_Skill10_Desc", "{0}초 동안 받는 데미지를 감소시킵니다." },
            {"jp_Skill10_Desc", "{0}秒間盾で受けるダメージを減少させます。" },
            {"en_Skill10_Desc", "Reduces incoming damage for {0} seconds." },

            {"kr_Skill11","방패치기" },
            {"jp_Skill11","シールドアタック" },
            {"en_Skill11","Shield Attack" },
            {"kr_Skill11_Desc", "방패로 적을 공격하여 {0}의 데미지와 {1}초 동안 스턴 부여." },
            {"jp_Skill11_Desc", "盾で敵を攻撃して {0}ダメージと {1}秒間スタンを与えます。" },
            {"en_Skill11_Desc", "Attack the enemy with a shield to deal {0}damage and stun for {1}seconds." },

            //마법사 스킬 리스트 12.파이어 볼 13.블레이징 플레임 14.뇌격(라이트닝 스트라이크) 15.체인 라이트닝 16.차원 왜곡 17.정화의 빛 18.빙결화(프리징 블라스트) 19.치명적인 공격(critical attack) 20.재빠른 발놀림 21.매직 쉴드
            {"kr_Skill12","파이어 볼" },
            {"jp_Skill12","ファイアボール" },
            {"en_Skill12","Fire Ball" },
            {"kr_Skill12_Desc", "적에게 {0}의 데미지를 입히며 일정 확률로 {1}초 동안 화상 부여." },
            {"jp_Skill12_Desc", "敵に {0}ダメージを与え、一定確率で {1}秒間燃焼状態にさせる。" },
            {"en_Skill12_Desc", "Inflicts {0} damage to the enemy and has a chance to get burn debuff for {1} seconds." },

            {"kr_Skill13","블레이징 플레임" },
            {"jp_Skill13","ブレイジングフレイム" },
            {"en_Skill13","Blazing Flame" },
            {"kr_Skill13_Desc", "커다란 불꽃 회오리가 {0}의 데미지를 입히며 일정 확률로 {1}초 동안 화상 부여." },
            {"jp_Skill13_Desc", "大きな炎の渦が {0}ダメージを与え、一定確率で {1}秒間燃焼状態にさせる。" },
            {"en_Skill13_Desc", "A large flame vortex deals {0} damage and has a chance to get burn debuff for {1} seconds."},


            {"kr_Skill14","뇌격" },
            {"jp_Skill14","ライトニングストライク" },
            {"en_Skill14","Lightning Strike" },
            {"kr_Skill14_Desc", "소환한 번개가 내려, {0}의 데미지를 입히며 일정 확률로 {1}초 동안 감전 부여." },
            {"jp_Skill14_Desc", "召喚した稲妻が降り、 {0}ダメージを与え、一定確率で {1}秒間感電状態にさせる。" },
            {"en_Skill14_Desc", "A lightning bolt summoned descends, inflicting {0} damage and has a chance to get electrocuted for {1} seconds." },

            {"kr_Skill15","메테오" } ,
            {"jp_Skill15","メテオ" },
            {"en_Skill15","Meteor" },
            {"kr_Skill15_Desc", "하늘에서 떨어지는 운석이 {0}의 데미지를 입히며, {1}초 동안 화상 부여." },
            {"jp_Skill15_Desc", "空から落ちる隕石が {0}ダメージを与え、 {1}秒間燃焼状態にさせる。" },
            {"en_Skill15_Desc", "A meteor falling from the sky deals {0} damage and gives burn debuff for {1} seconds." },


            {"kr_Skill16","차원 왜곡" },
            {"jp_Skill16","ディメンションワープ" },
            {"en_Skill16","Dimensional Distortion" },
            {"kr_Skill16_Desc", "중력을 왜곡 시키는 구를 소환해 모든 적에게 {0}의 관통형 데미지를 입힌다." },
            {"jp_Skill16_Desc", "重力を歪ませる球を召喚し、全ての敵に {0}の貫通ダメージを与えます。" },
            {"en_Skill16_Desc", "Summons a sphere that distorts gravity to deal {0} penetrating damage to all enemies." },

            {"kr_Skill17","정화의 빛" },
            {"jp_Skill17","ホーリーレイ" },
            {"en_Skill17","Holy ray" },
            {"kr_Skill17_Desc", "신앙을 정제시켜 실체화한 모습을 불러내어 모든 적에게 {0}의 데미지를 입히며, 파티 전원의 체력을 {1}만큼 회복시킨다." },
            {"jp_Skill17_Desc", "信仰を浄化し、実体化した姿を呼び出し、全ての敵に {0}のダメージを与え、パーティ全員の体力を {1}回復させます。" },
            {"en_Skill17_Desc", "Purify faith and summon a physical form of it to deal {0} damage to all enemies and recover the health of all party members by {1}." },

            {"kr_Skill18","빙결화" },
            {"jp_Skill18","フリージングブラスト" },
            {"en_Skill18","Freezing Blast" },
            {"kr_Skill18_Desc", "닿는 모든것을 얼려버리는 냉기를 소환해 {0}의 데미지를 입히며, {1}초 동안 슬로우 부여." },
            {"jp_Skill18_Desc", "触れるもの全てを凍らせる冷気を召喚し、 {0}のダメージを与え、 {1}秒間スローを与えます。" },
            {"en_Skill18_Desc", "Summons a cold that freezes everything it touches to deal {0} damage and slow for {1} seconds." },

            {"kr_Skill19","마나 리제너레이션" },
            {"jp_Skill19","マナリジェネレーション" },
            {"en_Skill19","Mana Regeneration" },
            {"kr_Skill19_Desc", "마나를 즉시 최대치로 회복한다." },
            {"jp_Skill19_Desc", "マナを即座に最大まで回復します。" },
            {"en_Skill19_Desc", "Recovers full mana immediately." },


            {"kr_Skill20","신체 활성화" },
            {"jp_Skill20","ボディアクティベーション" },
            {"en_Skill20","Body Activation" },
            {"kr_Skill20_Desc", "신체의 활성화로 {0}초 동안 스테미너 회복 속도와 마나 회복 속도를 상승시킨다.." },
            {"jp_Skill20_Desc", "体の活性化で {0}秒間スタミナ回復速度とマナ回復速度を上昇させます。" },
            {"en_Skill20_Desc", "Increases stamina recovery rate and mana recovery rate for {0} seconds by activating the body." },

            {"kr_Skill21","매직 쉴드" },
            {"jp_Skill21","マジックシールド" },
            {"en_Skill21","Magic Shield" },
            {"kr_Skill21_Desc", "마법으로 만든 방패를 통해, {0}초 동안 받는 데미지를 감소시킵니다." },
            {"jp_Skill21_Desc", "魔法で作った盾を通じて、{0}秒間受けるダメージを減少させます。" },
            {"en_Skill21_Desc", "Reduces incoming damage for {0} seconds through a shield made of magic." },

        };
    }

    public string GetLocalizedString(string key)
    {
        //UI등에서 가져올 텍스트 데이터의 경우
        //text.text = LocalizationManager.Instance.GetLocalizedString(ItemData.itemNameKey); 으로 사용.

        string fullKey = $"{currentLanguage}_{key}";
        if (localizedTexts.TryGetValue(fullKey, out string localizedText))
        {
            return localizedText;
        }

        // 키가 없을 경우 기본값 반환
        return key;
    }

    //스킬 카드나 아이템 카드의 경우 여러개의 value를 받아야 하기 때문에 아래의 메서드 사용.
    public string GetLocalizedFormattedString(string key, params object[] args)//"{0}만큼의 데미지를{1}초간 입힙니다." 이런식으로 사용 가능(key,value1,value2)
    {
        // 현재 언어와 키를 조합하여 완전한 키 생성
        string fullKey = $"{currentLanguage}_{key}";

        // 딕셔너리에서 문자열 검색
        if (localizedTexts.TryGetValue(fullKey, out string localizedText))
        {
            try
            {
                // 문자열 포맷팅 적용
                return string.Format(localizedText, args);
            }
            catch (FormatException e)
            {
                Debug.LogError($"[LocalizationManager] Format error in key: {fullKey}, Error: {e.Message}");
                return localizedText; // 포맷 오류 발생 시 원본 텍스트 반환
            }
        }

        // 키가 없을 경우 기본값 반환
        Debug.LogWarning($"[LocalizationManager] Key not found: {fullKey}");
        return key;
    }

    public void SetLanguage(string languageCode)//해당 함수를 호출하여 언어 변경
    {
        currentLanguage = languageCode;
        // en, kr, jp 등의 언어 코드를 받아서 변경

        // PlayerPrefs에 저장
        PlayerPrefs.SetString("Language", languageCode);
        PlayerPrefs.Save();

        Debug.Log($"언어가 변경되었습니다: {languageCode}");

        // 변경된 언어를 UI에 반영
        UpdateAllLocalizedTexts();
    }

    public void LoadLanguage()
    {
        // 저장된 언어가 있다면 로드, 없으면 기본값 사용
        currentLanguage = PlayerPrefs.GetString("Language", defaultLanguage);
        Debug.Log($"언어 로드: {currentLanguage}");
    }

    private void UpdateAllLocalizedTexts()
    {
        // UI 텍스트 컴포넌트를 모두 찾아서 갱신
        LocalizedText[] localizedTextComponents = FindObjectsOfType<LocalizedText>();
        foreach (var localizedText in localizedTextComponents)
        {
            localizedText.UpdateText();
        }
    }
}
