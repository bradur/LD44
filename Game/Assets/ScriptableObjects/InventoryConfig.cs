using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewInventoryConfig", menuName = "Custom/New InventoryConfig")]
public class InventoryConfig : ScriptableObject
{

    [SerializeField]
    private int initialCurrency = 100;
    private int currency = 100;
    public int Currency { get { return currency; } set { currency = value; } }

    [SerializeField]
    private List<InventoryItem> purchasedItems;

    public List<InventoryItem> PurchasedItems { get { return purchasedItems; } }

    [SerializeField]
    private List<InventoryItem> availableItems;
    public List<InventoryItem> AvailableItems { get { return availableItems; } }

    private List<InventoryItem> unlockedItems = new List<InventoryItem>();

    private List<InventoryItem> lockedItems;

    [SerializeField]
    private List<InventoryItem> initialItems;

    public List<InventoryItem> InitialItems { get { return initialItems; } }

    private InventoryItem selectedWeapon;

    public void UnlockItem(InventoryItem item) {
        foreach(InventoryItem unlockedItem in unlockedItems) {
            if (unlockedItem.WeaponConfig == item.WeaponConfig) {
                return;
            }
        }
        InventoryItem newItem = new InventoryItem(item.WeaponConfig, item.Ammo, item.UnlimitedAmmo, item.SelectOrder);
        unlockedItems.Add(newItem);
    } 

    public void Init(bool gameStart)
    {
        if (gameStart)
        {
            unlockedItems.Clear();
        }
        currency = initialCurrency;
        ClearItems();
        foreach (InventoryItem item in initialItems)
        {
            item.Purchased = false;
            InventoryItem newItem = new InventoryItem(item.WeaponConfig, item.Ammo, item.UnlimitedAmmo, item.SelectOrder);
            availableItems.Add(newItem);
        }
        foreach (InventoryItem item in unlockedItems)
        {
            item.Purchased = false;
            InventoryItem newItem = new InventoryItem(item.WeaponConfig, item.Ammo, item.UnlimitedAmmo, item.SelectOrder);
            availableItems.Add(newItem);
        }
    }

    public void ClearItems()
    {
        purchasedItems.Clear();
        foreach (InventoryItem item in availableItems)
        {
            item.Purchased = false;
        }
        availableItems.Clear();
    }

    public void ResetCurrency()
    {
        currency = 100;
    }

    public bool PurchaseItem(InventoryItem item)
    {

        if (!item.Purchased && currency > item.WeaponConfig.BasePrice)
        {
            currency -= item.WeaponConfig.BasePrice;
            item.Purchased = true;
            purchasedItems.Add(item);
            //InventoryItem newItem = new InventoryItem(item.WeaponConfig, item.Ammo);
            //purchasedItems.Add(newItem);
            return true;
        }
        return false;
    }
    public bool PurchaseAmmo(InventoryItem item)
    {
        if (item.Purchased && currency > item.WeaponConfig.Projectile.BasePrice)
        {
            currency -= item.WeaponConfig.Projectile.BasePrice;
            foreach (InventoryItem purchasedItem in purchasedItems)
            {
                if (purchasedItem == item)
                {
                    purchasedItem.Ammo += 1;
                    break;
                }
            }
            return true;
        }
        return false;
    }

    public void SelectItem(InventoryItem item)
    {
        selectedWeapon = item;
    }

    public InventoryItem GetCurrentItem()
    {
        return selectedWeapon;
    }

}

[System.Serializable]
public class InventoryItem : System.Object
{
    public InventoryItem(WeaponConfig weaponConfig, int ammoCount, bool unlimitedAmmo, int selectOrder)
    {
        this.weaponConfig = weaponConfig;
        ammo = ammoCount;
        this.unlimitedAmmo = unlimitedAmmo;
        this.selectOrder = selectOrder;
    }

    [SerializeField]
    private bool purchased = false;
    public bool Purchased { get { return purchased; } set { purchased = value; } }

    [SerializeField]
    private WeaponConfig weaponConfig;

    public WeaponConfig WeaponConfig { get { return weaponConfig; } }

    [SerializeField]
    private int selectOrder = 0;
    public int SelectOrder { get { return selectOrder; } }

    private Weapon weapon;
    public Weapon Weapon { get { return weapon; } set { weapon = value; } }

    [SerializeField]
    private int ammo = 5;
    public int Ammo { get { return ammo; } set { ammo = value; } }

    [SerializeField]
    private bool unlimitedAmmo = false;
    public bool UnlimitedAmmo { get { return unlimitedAmmo; } }


}