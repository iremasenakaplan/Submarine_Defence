

using TMPro;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

    public class LeaderboardShowcase : MonoBehaviour
    {
        dreamloLeaderBoard dl;
        [SerializeField] private TextMeshProUGUI _playerScoreText;
        //[SerializeField] private TextMeshProUGUI[] _entryFields;
        [SerializeField] private Transform _leaderboardParent;
        [SerializeField] private GameObject _leaderboardEntry;
        
        [SerializeField] private TMP_InputField _playerUsernameInput;
       // [SerializeField] private TMP_InputField _countryInput;
        [SerializeField] TMP_Dropdown countriesDropdown;
       // [SerializeField] LobyMenuManager lobyManager;

        string name = "";
        string country = "";
        int countryIndex = 0;
        
        private void Start()
        {
            this.dl = dreamloLeaderBoard.GetSceneDreamloLeaderboard();
            

            name = PlayerPrefs.GetString(Application.identifier+"User");
            country = PlayerPrefs.GetString(Application.identifier+"Country");
            countryIndex = PlayerPrefs.GetInt(Application.identifier+"CountryIndex");
        
            countriesDropdown.ClearOptions();
            countriesDropdown.AddOptions(mCountries);
       
            countriesDropdown.value = countryIndex;

            if(name!=""){
                _playerUsernameInput.text = name;
                Submit();
            }
            
            AddPlayerScore();
                  
        }

     

        public void AddPlayerScore()
        {
            int currentLevel = PlayerPrefs.GetInt(Application.identifier+"Level");
            _playerScoreText.text = "Your score: " + currentLevel;
        }
        
        

        public void Load(){
            int count = 0;
            List<dreamloLeaderBoard.Score> scoreList = dl.ToListHighToLow();
            foreach (Transform child in _leaderboardParent) {
                GameObject.Destroy(child.gameObject);
            }
            foreach (dreamloLeaderBoard.Score currentScore in scoreList)
            {
                count++;
                GameObject ld = Instantiate(_leaderboardEntry, _leaderboardParent) ;
                ld.GetComponent<TextMeshProUGUI>().text = $"{count}. ";
                string name = currentScore.playerName.Replace("+", " ");;
                ld.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{name}";
                ld.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = $"{currentScore.score.ToString()}";
                ld.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = $"{currentScore.shortText.ToString()}";
                if(count==1){
                    ld.transform.GetChild(2).GetChild(0).gameObject.SetActive(true);
                    ld.transform.GetChild(2).GetChild(1).gameObject.SetActive(true);
                    ld.transform.GetChild(2).GetChild(2).gameObject.SetActive(true);
                }else if(count==2){
                    ld.transform.GetChild(2).GetChild(1).gameObject.SetActive(true);
                    ld.transform.GetChild(2).GetChild(2).gameObject.SetActive(true);
                }else if(count==3){
                    ld.transform.GetChild(2).GetChild(2).gameObject.SetActive(true);
                }
                int bound;
                // if(currentScore.score/5 >= lobyManager.armyRanks.Length)
                //     bound = lobyManager.armyRanks.Length-1;
                // else 
                //     bound = (currentScore.score/5)%lobyManager.armyRanks.Length;
                // ld.transform.GetChild(3).GetComponent<Image>().sprite = lobyManager.armyRanks[bound];

                
                if (count >= 200) break;
            }
        }
        
         //=> LeaderboardCreator.GetLeaderboard(_leaderboardPublicKey, OnLeaderboardLoaded);

        // private void OnLeaderboardLoaded(Entry[] entries)
        // {
        //     // foreach (var entryField in _entryFields)
        //     // {
        //     //     entryField.text = "";
        //     // }
        //      foreach (Transform child in _leaderboardParent) {
        //         GameObject.Destroy(child.gameObject);
        //     }

        //     for (int i = 0; i < entries.Length; i++)
        //     {
        //         GameObject ld = Instantiate(_leaderboardEntry, _leaderboardParent) ;
        //         ld.GetComponent<TextMeshProUGUI>().text = $"{i+1}. {entries[i].Username} : {entries[i].Score}";
        //     }
        // }

        public void Submit()
        {
            if(_playerUsernameInput.text != "" ){
                int currentLevel = PlayerPrefs.GetInt(Application.identifier+"Level");
                PlayerPrefs.SetString(Application.identifier+"User", _playerUsernameInput.text);
                PlayerPrefs.SetString(Application.identifier+"Country", mCountries[countriesDropdown.value]);
                PlayerPrefs.SetInt(Application.identifier+"CountryIndex", countriesDropdown.value);
                dl.AddScore(_playerUsernameInput.text, currentLevel, 0, mCountries[countriesDropdown.value]);
            }
            StartCoroutine(LoadData());
            // LeaderboardCreator.UploadNewEntry(_leaderboardPublicKey, "Seymuaar", 23, Callback);
        }

        IEnumerator LoadData(){
            yield return new WaitForSeconds(2.0f);
            Load();
        }
        
        // public void DeleteEntry()
        // {
        //     LeaderboardCreator.DeleteEntry(_leaderboardPublicKey, Callback);
        // }
        
        // private void Callback(bool success)
        // {
        //     if (success)
        //         Load();
        // }

    List<string> mCountries = new List<string>
    {
            "Select Country",
            "Afghanistan",
            "Åland Islands",
            "Albania",
            "Algeria",
            "American Samoa",
            "Andorra",
            "Angola",
            "Anguilla",
            "Antarctica",
            "Antigua and Barbuda",
            "Argentina",
            "Armenia",
            "Aruba",
            "Australia",
            "Austria",
            "Azerbaijan",
            "Bahamas",
            "Bahrain",
            "Bangladesh",
            "Barbados",
            "Belarus",
            "Belgium",
            "Belize",
            "Benin",
            "Bermuda",
            "Bhutan",
            "Bolivia",
            "Bosnia and Herzegovina",
            "Botswana",
            "Bouvet Island",
            "Brazil",
            "British Indian Ocean Territory",
            "Brunei Darussalam",
            "Bulgaria",
            "Burkina Faso",
            "Burundi",
            "Cambodia",
            "Cameroon",
            "Canada",
            "Cape Verde",
            "Cayman Islands",
            "Central African Republic",
            "Chad",
            "Chile",
            "China",
            "Christmas Island",
            "Cocos (Keeling) Islands",
            "Colombia",
            "Comoros",
            "Congo",
            "Congo, The Democratic Republic of The",
            "Cook Islands",
            "Costa Rica",
            "Cote D'ivoire",
            "Croatia",
            "Cuba",
            "Curaçao",
            "Cyprus",
            "Czech Republic",
            "Denmark",
            "Djibouti",
            "Dominica",
            "Dominican Republic",
            "Ecuador",
            "Egypt",
            "El Salvador",
            "Equatorial Guinea",
            "Eritrea",
            "Estonia",
            "Ethiopia",
            "Falkland Islands (Malvinas)",
            "Faroe Islands",
            "Fiji",
            "Finland",
            "France",
            "French Guiana",
            "French Polynesia",
            "French Southern Territories",
            "Gabon",
            "Gambia",
            "Georgia",
            "Germany",
            "Ghana",
            "Gibraltar",
            "Greece",
            "Greenland",
            "Grenada",
            "Guadeloupe",
            "Guam",
            "Guatemala",
            "Guernsey",
            "Guinea",
            "Guinea-bissau",
            "Guyana",
            "Haiti",
            "Heard Island and Mcdonald Islands",
            "Holy See (Vatican City State)",
            "Honduras",
            "Hong Kong",
            "Hungary",
            "Iceland",
            "India",
            "Indonesia",
            "Iran, Islamic Republic of",
            "Iraq",
            "Ireland",
            "Isle of Man",
            "Israel",
            "Italy",
            "Jamaica",
            "Japan",
            "Jersey",
            "Jordan",
            "Kazakhstan",
            "Kenya",
            "Kiribati",
            "Korea, Democratic People's Republic of",
            "Korea, Republic of",
            "Kuwait",
            "Kyrgyzstan",
            "Lao People's Democratic Republic",
            "Latvia",
            "Lebanon",
            "Lesotho",
            "Liberia",
            "Libyan Arab Jamahiriya",
            "Liechtenstein",
            "Lithuania",
            "Luxembourg",
            "Macao",
            "Macedonia, The Former Yugoslav Republic of",
            "Madagascar",
            "Malawi",
            "Malaysia",
            "Maldives",
            "Mali",
            "Malta",
            "Marshall Islands",
            "Martinique",
            "Mauritania",
            "Mauritius",
            "Mayotte",
            "Mexico",
            "Micronesia, Federated States of",
            "Moldova, Republic of",
            "Monaco",
            "Mongolia",
            "Montenegro",
            "Montserrat",
            "Morocco",
            "Mozambique",
            "Myanmar",
            "Namibia",
            "Nauru",
            "Nepal",
            "Netherlands",
            "New Caledonia",
            "New Zealand",
            "Nicaragua",
            "Niger",
            "Nigeria",
            "Niue",
            "Norfolk Island",
            "Northern Mariana Islands",
            "Norway",
            "Oman",
            "Pakistan",
            "Palau",
            "Palestinian Territory, Occupied",
            "Panama",
            "Papua New Guinea",
            "Paraguay",
            "Peru",
            "Philippines",
            "Pitcairn",
            "Poland",
            "Portugal",
            "Puerto Rico",
            "Qatar",
            "Reunion",
            "Romania",
            "Russia",
            "Rwanda",
            "Saint Helena",
            "Saint Kitts and Nevis",
            "Saint Lucia",
            "Saint Pierre and Miquelon",
            "Saint Vincent and The Grenadines",
            "Samoa",
            "San Marino",
            "Sao Tome and Principe",
            "Saudi Arabia",
            "Senegal",
            "Serbia",
            "Seychelles",
            "Sierra Leone",
            "Singapore",
            "Slovakia",
            "Slovenia",
            "Solomon Islands",
            "Somalia",
            "South Africa",
            "South Georgia and The South Sandwich Islands",
            "Spain",
            "Sri Lanka",
            "Sudan",
            "Suriname",
            "Svalbard and Jan Mayen",
            "Eswatini",
            "Sweden",
            "Switzerland",
            "Syrian Arab Republic",
            "Taiwan (ROC)",
            "Tajikistan",
            "Tanzania, United Republic of",
            "Thailand",
            "Timor-leste",
            "Togo",
            "Tokelau",
            "Tonga",
            "Trinidad and Tobago",
            "Tunisia",
            "Turkey",
            "Turkmenistan",
            "Turks and Caicos Islands",
            "Tuvalu",
            "Uganda",
            "Ukraine",
            "United Arab Emirates",
            "United Kingdom",
            "United States",
            "United States Minor Outlying Islands",
            "Uruguay",
            "Uzbekistan",
            "Vanuatu",
            "Venezuela",
            "Vietnam",
            "Virgin Islands, British",
            "Virgin Islands, U.S.",
            "Wallis and Futuna",
            "Western Sahara",
            "Yemen",
            "Zambia",
            "Zimbabwe"
       	};
    }

