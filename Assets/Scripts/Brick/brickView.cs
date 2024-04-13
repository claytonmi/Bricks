using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brickView : MonoBehaviour
{
	// Start is called before the first frame update
	private brickController _brickController;
    private SpriteRenderer _spriteRenderer;

    public Sprite Brick;
    public Sprite Brick2;
    public Sprite Brick3;
    public Sprite Brick4;

    private int currentEnemy = 0; // Indica o índice atual para enemy3Sprites
    private int currentEnemy2 = 0; // Indica o índice atual para enemy4Sprites

    void Start()
    {
        InitializeComponents();
    }

    private void InitializeComponents()
    {
        _brickController = GetComponent<brickController>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        if (_brickController == null)
        {
            Debug.LogError("BrickController não encontrado em " + gameObject.name);
        }
    }

	public void PerformTakeDamage(float damage, Collision2D collision)
	{
		_brickController.TakeDamage(damage, collision);

        switch (gameObject.tag)
        {
            case "Enemy2":
                // Defina a imagem do SpriteRenderer para o sprite do enemy1
                _spriteRenderer.sprite = Brick4;
                break;
            case "Enemy":
                // Alterne entre as imagens para o enemy2
                if (currentEnemy == 0)
                {
                    _spriteRenderer.sprite = Brick2;
                    currentEnemy++;
                }
                else if (currentEnemy == 1)
                {
                    _spriteRenderer.sprite = Brick4;
                    currentEnemy++;
                }
                break;
            case "Enemy3":
                _spriteRenderer.sprite = Brick4;
                break;
            case "Enemy4":
                // Se houver mais de uma imagem definida para o enemy4, faça a troca de imagem em ordem
                if (currentEnemy2 == 0)
                {
                    _spriteRenderer.sprite = Brick2;
                    currentEnemy2++;
                }
                else if (currentEnemy2 == 1)
                {
                    _spriteRenderer.sprite = Brick3;
                    currentEnemy2++;
                }
                else if (currentEnemy2 == 2)
                {
                    _spriteRenderer.sprite = Brick4;
                    currentEnemy2++;
                }
                break;
        }
    }
}