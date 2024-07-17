using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Quantum;

public class InGameUIHandler : MonoBehaviour
{
    [SerializeField]
    private TMP_Text m_healthValueText;
    [SerializeField]
    private Image m_healthBarImage;
    [SerializeField]
    private GameObject m_deathPanel;

    [SerializeField]
    private float m_maxHealth = 0;

    private EntityView m_entityView = null;

    private void OnEnable()
    {
        GameEvents.OnEntityViewCreated += OnEntityViewCreated;
    }

    private void OnDisable()
    {
        GameEvents.OnEntityViewCreated -= OnEntityViewCreated;
    }

    private void OnEntityViewCreated(EntityView entityView)
    {
        m_entityView = entityView;
        if (Utils.TryGetQuantumFrame(out Frame frame))
        {
            var healthComponent = frame.Get<HealthComponent>(m_entityView.EntityRef);
            m_maxHealth = (int)healthComponent.maxHealth;
            m_healthValueText.text = ((int)healthComponent.currentHealth).ToString() + "/" + m_maxHealth;
            m_healthBarImage.fillAmount = ((float)healthComponent.currentHealth / m_maxHealth);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_entityView)
        {
            if (Utils.TryGetQuantumFrame(out Frame frame))
            {
                var healthComponent = frame.Get<HealthComponent>(m_entityView.EntityRef);

                m_healthValueText.text = ((int)healthComponent.currentHealth).ToString() + "/" + m_maxHealth;
                m_healthBarImage.fillAmount = ((float)healthComponent.currentHealth / m_maxHealth);
            }
        }
        if (Utils.TryGetQuantumFrame(out Frame frame2))
        {
            if (frame2.TryGetSingletonEntityRef<GameSession>(out var enity))
            {
                var gameSession = frame2.GetSingleton<GameSession>();

                if (gameSession.State == Quantum.GameState.DeathScreen)
                {
                    m_deathPanel.SetActive(true);
                }
                else
                {
                    m_deathPanel.SetActive(false);
                }
            }
        }
    }
}
