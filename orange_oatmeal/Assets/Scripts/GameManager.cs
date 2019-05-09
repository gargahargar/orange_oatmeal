using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int maxMessages = 25;
    [SerializeField]
//    string username = "Kevin";
    
    PlayerScript ps;

    public GameObject chatPanel, textObject; 
    public InputField chatBox;
    public Color commandColor;
    public Color infoColor;
    public Color roomTitleColor;
    public Color roomDescColor;
    public Color spaceTitleColor;
    public Color spaceDescColor;

    [SerializeField]
    List<Message> messageList = new List<Message>();

    // Start is called before the first frame update
    void Start()
    {
        ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
    }
    private void ProcessCommand(string command)
    {
        command = command.ToUpper();
        for(int i = 0; i < 12; i++)
        {
            if (command.Equals(Enum.GetName(typeof(Exit), i)))
            {
                List<string> outputList = ps.Move(i);
                if (outputList.Count == 1) // Cant move command
                    SendMessageToChat(outputList[0], Message.MessageType.info);
                else if (outputList.Count == 2) // New Space
                {
                    SendMessageToChat(outputList[0], Message.MessageType.spacetitle);
                 //   SendMessageToChat(outputList[1], Message.MessageType.spacedescription);
                }
                else if (outputList.Count == 4) // new Room and new space
                {
                    SendMessageToChat(outputList[0], Message.MessageType.roomtitle);
                    SendMessageToChat(outputList[1], Message.MessageType.roomdescription);
                    SendMessageToChat(outputList[2], Message.MessageType.spacetitle);
                 //   SendMessageToChat(outputList[3], Message.MessageType.spacedescription);
                }
            }
        }
        if(command.Equals("LS"))
        {
            List<string> outputList = ps.LookSpace();
            SendMessageToChat(outputList[0], Message.MessageType.spacetitle);
            SendMessageToChat(outputList[1], Message.MessageType.spacedescription);
        }
        if (command.Equals("LR"))
        {
            List<string> outputList = ps.LookRoom();
            SendMessageToChat(outputList[0], Message.MessageType.roomtitle);
            SendMessageToChat(outputList[1], Message.MessageType.roomdescription);
        }
    }
    // Update is called once per frame
    void Update() {

        if(chatBox.text != "")
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SendMessageToChat(": " + chatBox.text, Message.MessageType.command);
                ProcessCommand(chatBox.text);
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
        Color color = infoColor;
        switch (messageType)
        {
            case Message.MessageType.command:
                color = commandColor;
                break;
            case Message.MessageType.info:
                color = infoColor;
                break;
            case Message.MessageType.roomtitle:
                color = roomTitleColor;
                break;
            case Message.MessageType.roomdescription:
                color = roomDescColor;
                break;
            case Message.MessageType.spacetitle:
                color = spaceTitleColor;
                break;
            case Message.MessageType.spacedescription:
                color = spaceDescColor;
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
        command,
        info,
        roomtitle,
        roomdescription,
        spacetitle,
        spacedescription
    }

}
