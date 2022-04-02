using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TagFiller : MonoBehaviour
{
    [SerializeField] GameObject tagButton, selectedButton;
    [SerializeField] TextAsset tagFile;
    [SerializeField] float colMult, rowStart, colStart, spaces;
    [SerializeField] TMP_InputField inputText;

    private Button _toDelete;
    GameObject sel;

    List<GameObject> buttonsList = new List<GameObject>();
    List<string> tagsList = new List<string>();
    List<string> allTagsList = new List<string>();
    static List<string> selectedButtons = new List<string>();

    public static Dictionary<string, int> tagsFrequency = new Dictionary<string, int>() { };

    int currRow = 0, currCol = 0;
    bool first = true, searching = false;
    float rowWidth = 0;


    public static List<string> GetSelectedTags()
    {
        return selectedButtons;
    }

    public static Dictionary<string, int> GetTagsDictionary()
    {
        return tagsFrequency;
    }

    public static void SetTagsDictionary(Dictionary<string, int> _tagsFrequency)
    {
        tagsFrequency = _tagsFrequency;
        Debug.Log("SET DICTIONARY SUCCESS " + tagsFrequency.Count);
        
    }

    public IEnumerator Start()
    {
        //// Load tags from file
        //var content = tagFile.text;
        //string[] _tags = content.Split('\n');
        //tagsList = new List<string>(_tags);
       
        if (transform.childCount> 0)
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }
        

        // Sort list of tags by frequency
        if (!searching)
        {
            GetMostCommonTag();
        } else
        {
            first = true;
            
        }

        // Make the buttons in the canvas first
        MakeButtons();

        Canvas.ForceUpdateCanvases();
        yield return null;

        // Put the buttons in the correct rows and columns.
        Debug.Log("Gone past");
        RestructureButtons();

        searching = false;
        rowWidth = 0;

        
    }

    /*** Instantiates the buttons in the scrollable list.
     */
    public void MakeButtons()
    {
       
        currRow = 0;
        currCol = 0;
        rowWidth = 0;
        first = true;


        // Show selected buttons first.
        if (selectedButtons.Count > 0)
        {
            foreach (string selected in selectedButtons)
            {
                _toDelete = CreateOneButton(selected, selectedButton).GetComponent<Button>();
                _toDelete.onClick.AddListener(UnselectTag);
            }
        }
        

        // Make all the buttons first so that we can calculate the widths properly.
        foreach (string tag in tagsList)
        {
            if (!selectedButtons.Contains(tag))
            {
                CreateOneButton(tag, tagButton).GetComponent<Button>().onClick.AddListener(SelectTag);
            }
            
        }

        // Instantiate add new button
        CreateOneButton("+ add new", tagButton).GetComponent<Button>().onClick.AddListener(CreateNewTag);

    }

    public void RestructureButtons()
    {
        Debug.Log("Restr");
        Debug.Log(buttonsList.Count());

        foreach (GameObject _butt in buttonsList)
        {
            Canvas.ForceUpdateCanvases();

            string _tag = _butt.GetComponentInChildren<TextMeshProUGUI>().text;

            Vector3 pos = new Vector3(rowStart, colStart, -1) + RowHandler(_tag, _butt);

            rowWidth += GetButtonWidth(_butt);

            _butt.transform.localPosition = pos;


            first = false;
        }
    }

    public Vector3 RowHandler(string currTag, GameObject _butt)
    {
        
        if (rowWidth + GetButtonWidth(_butt) < 100)
        {
            // Add button to new column
            if (first)
            {
                return new Vector3(0, 0, 0);
            }

            currCol += 1;
            rowWidth += spaces*currCol;

            return new Vector3(rowWidth, colMult * currRow, 0);

        }
        else
        {
            // Enter new row
            rowWidth = 0;
            currRow += 1;
            currCol = 0;
            return new Vector3(0, colMult * currRow, 0);
        }
    }

    float GetButtonWidth(GameObject butt)
    {
        // Now works after forcing the canvas to update. Can't do this in Start()
        return butt.GetComponent<RectTransform>().sizeDelta.x;

    }

    void GetMostCommonTag()
    {
        // Order based on frequency
        allTagsList = tagsFrequency.OrderByDescending(x => x.Value).Select(x => x.Key).ToList();


        // Show 6 most common tags
        if (allTagsList.Count >= 5)
        {
            tagsList = allTagsList.GetRange(0,5);
        } else
        {
            tagsList = allTagsList;
        }

    }

    public void SearchTag()
    {
        searching = true;
        tagsList = new List<string>();
        buttonsList = new List<GameObject>();
        
        
        if (inputText.text.Trim('\n', ' ', (char)8203).ToString().Length == 0)
        {
            searching = false;

        } else
        {
            foreach (string word in allTagsList)
            {
                string newWord = word.ToString();
                string toCheck = inputText.text.ToLower().Trim('\n', ' ', (char)8203).ToString();

                if (newWord.Contains(toCheck) && !selectedButtons.Contains(word))
                {
                    tagsList.Add(word);

                }
            }
        }

        StartCoroutine(Start());
        

    }

    private GameObject CreateOneButton(string _text, GameObject style)
    {

        style.GetComponentInChildren<TextMeshProUGUI>().text = _text;
        GameObject _firstbutton = Instantiate(style, transform);
        buttonsList.Add(_firstbutton);

        return _firstbutton;
    }

    /***
     * Handles new tag creation 
     */
    public void CreateNewTag() {

        // Grab new tag from input field
        string textInput = inputText.text.ToLower().Trim('\n', ' ', (char)8203).ToString();

        // When + add new button is pressed, create a new button and immediately select it.
        if (textInput.Length == 0)
        {
            inputText.placeholder.GetComponent<TextMeshProUGUI>().text = "Enter new tag";

        } else
        {
  
            searching = true;
            inputText.text = "";
            selectedButtons.Add(textInput);

            first = true;
            tagsList = new List<string>();
            buttonsList = new List<GameObject>();

            StartCoroutine(Start());
        }
        
    }

    public void UnselectTag()
    {
        sel = EventSystem.current.currentSelectedGameObject;
        sel.gameObject.SetActive(false);
        selectedButtons.Remove(sel.GetComponentInChildren<TextMeshProUGUI>().text);

        // USE THIS DON'T USE START() IDK.
        SearchTag();
    }

    public void SelectTag()
    {
        sel = EventSystem.current.currentSelectedGameObject;
        selectedButtons.Add(sel.GetComponentInChildren<TextMeshProUGUI>().text);

        first = true;
        searching = true;
        tagsList = new List<string>();
        buttonsList = new List<GameObject>();
        StartCoroutine(Start());

    }


}
