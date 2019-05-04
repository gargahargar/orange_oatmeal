using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int maxMessages = 25;
    [SerializeField]
    string username = "Kevin";
    public GameObject chatPanel, textObject; 
    public InputField chatBox;
    public Color playerMessageColor;
    public Color infoMessageColor;

    [SerializeField]
    List<Message> messageList = new List<Message>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {

        if(chatBox.text != "")
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SendMessageToChat(username+ ": " + chatBox.text, Message.MessageType.player);
                chatBox.text = "";
                chatBox.ActivateInputField();
            }
        }else
        {
            if (!chatBox.isFocused && Input.GetKeyDown(KeyCode.Return))
            {
                chatBox.ActivateInputField();
            }
        }

        if(!chatBox.isFocused) {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                
                SendMessageToChat("You hit the orc for " + Random.Range(13, 34) +" damage!", Message.MessageType.info);
            }
        }

    }

    public void SendMessageToChat(string text, Message.MessageType messageType)
    {
        if (messageList.Count >= maxMessages)
        {
            Destroy(messageList[0].textObject.gameObject);
            messageList.Remove(messageList[0]);
        }

        Message newMessage = new Message();
        newMessage.text = text;

        GameObject newText = Instantiate(textObject, chatPanel.transform);
        newMessage.textObject = newText.GetComponent<Text>();

        newMessage.textObject.text = newMessage.text;
        newMessage.textObject.color = MessageTypedColor(messageType);
        messageList.Add(newMessage);
    }

    Color MessageTypedColor(Message.MessageType messageType)
    {
        Color color = infoMessageColor;
        switch (messageType)
        {
            case Message.MessageType.player:
                color = playerMessageColor;
                break;
            case Message.MessageType.info:
                color = infoMessageColor;
                break;
            default:
                break;
        }

        return color;
    }
}

[System.Serializable]
public class Message
{
    public string text;
    public Text textObject;
    public MessageType messageType;

    public enum MessageType
    {
        player,
        info
    }

}
