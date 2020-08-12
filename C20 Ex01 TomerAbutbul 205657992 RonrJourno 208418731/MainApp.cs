using System;
using System.Collections.Generic;
using System.Drawing.Design;
using System.Linq;
using System.Windows.Forms;
using FacebookWrapper.ObjectModel;
using FacebookWrapper;

namespace C20_Ex01_TomerAbutbul_205657992_RonJourno_208418731
{
    public partial class MainApp : Form
    {
        private User m_LoggedInUser = null;
        private List<CheckinDetails> m_checkinDetails = null;

        public MainApp()
        {
            InitializeComponent();
        }

        private void buttonLoginLogout_Click(object i_Sender, EventArgs i_E)
        {
            if(m_LoggedInUser != null)
            {
                logoutAndInit();

            }

            else
            {
                loginAndInit();
                if(m_LoggedInUser != null)
                {
                    buttonLoginLogout.Text = "Logout";
                }

                //else error box
            }
        }

        private void FetchFriends_LinkClicked(object i_Sender, LinkLabelLinkClickedEventArgs i_E)
        {
            fetchFriends();
        }

        private void listBoxFriends_SelectedFriend(object i_Sender, EventArgs i_E)
        {
            displaySelectedFriend();
        }

        private void linkLabelFetchPosts_LinkClicked(object i_Sender, LinkLabelLinkClickedEventArgs i_E)
        {
            fetchPosts();
        }

        private void listBoxPosts_SelectedPost(object i_Sender, EventArgs i_E)
        {
            fetchSelectedPosts();
        }

        private void LinkLabelDisplayCheckins_LinkClicked(object i_Sender, LinkLabelLinkClickedEventArgs i_E)
        {
            fetchCheckins();
        }

        private void linkLabelUserEvents_LinkClicked(object i_Sender, LinkLabelLinkClickedEventArgs i_E)
        {
            fetchEvents();
        }

        private void listBoxEvents_SelectedIndexChanged(object i_Sender, EventArgs i_E)
        {
            if(listBoxEvents.SelectedItems.Count == 1)
            {
                Event selectedEvent = listBoxEvents.SelectedItem as Event;
                pictureBoxEvents.LoadAsync(selectedEvent.PictureNormalURL);
            }
        }

        private void linkLabelLikedPages_LinkClicked(object i_Sender, LinkLabelLinkClickedEventArgs i_E)
        {
            fetchPages();
        }

        private void buttonSetStatus_Click(object i_Sender, EventArgs i_E)
        {
            Status postedStatus = m_LoggedInUser.PostStatus(textBoxStatus.Text);
            MessageBox.Show("Status Posted! ID: " + postedStatus.Id);
        }

        private void loginAndInit()
        {
            LoginResult result = FacebookService.Login(
                "654371695268712",
                "public_profile",
                "email",
                "publish_to_groups",
                "user_birthday",
                "user_age_range",
                "user_gender",
                "user_link",
                "user_tagged_places",
                "user_videos",
                "publish_to_groups",
                "groups_access_member_info",
                "user_friends",
                "user_events",
                "user_likes",
                "user_location",
                "user_photos",
                "user_posts",
                "user_hometown");

            if(!string.IsNullOrEmpty(result.AccessToken))
            {
                m_LoggedInUser = result.LoggedInUser;
                fetchUserInfo();
            }
            else
            {
                MessageBox.Show(result.ErrorMessage);
            }
        }

        private void logoutAndInit()
        {
            m_LoggedInUser = null;
            buttonLoginLogout.Text = "Login";
            pictureBoxProfilePicture = null;
            listBoxFriends.Items.Clear();
            pictureBoxDisplayFriend = null;
            listBoxDisplayPosts.Items.Clear();
            listBoxPostComments.Items.Clear();
            listBoxEvents.Items.Clear();
            pictureBoxEvents = null;
            listBoxPages.Items.Clear();
            pictureBoxPage = null;
            listBoxCheckins.Items.Clear();
            textBoxStatus.Text = "";
        }

        private void fetchUserInfo()
        {
            pictureBoxProfilePicture.LoadAsync(m_LoggedInUser.PictureNormalURL);
            if(m_LoggedInUser.Posts.Count > 0)
            {
                textBoxStatus.Text = m_LoggedInUser.Posts[0].Message;
            }
        }

        private void fetchFriends()
        {
            listBoxFriends.Items.Clear();
            listBoxFriends.DisplayMember = "Name";
            foreach(User friend in m_LoggedInUser.Friends)
            {
                listBoxFriends.Items.Add(friend);
                friend.ReFetch(DynamicWrapper.eLoadOptions.Full);//exception
            }

            if(m_LoggedInUser.Friends.Count == 0)
            {
                MessageBox.Show("No Friends to retrieve :(");
            }
        }

        private void displaySelectedFriend()
        {
            if(listBoxFriends.SelectedItems.Count == 1)
            {
                User selectedFriend = listBoxFriends.SelectedItem as User;
                if(selectedFriend.PictureNormalURL != null)
                {
                    pictureBoxDisplayFriend.LoadAsync(selectedFriend.PictureNormalURL);
                }
                else
                {
                    pictureBoxDisplayFriend.Image = pictureBoxDisplayFriend.ErrorImage;
                }
            }
        }

        private void fetchPosts()
        {
            foreach(Post post in m_LoggedInUser.Posts)
            {
                if(post.Message != null && !listBoxDisplayPosts.Items.Contains(post.Message))
                {
                    listBoxDisplayPosts.Items.Add(post.Message);
                }

                else if(post.Caption != null && !listBoxDisplayPosts.Items.Contains(post.Caption))
                {
                    listBoxDisplayPosts.Items.Add(post.Caption);
                }

                else
                {
                    if(!listBoxDisplayPosts.Items.Contains(string.Format("[{0}]", post.Type)))
                    {
                        listBoxDisplayPosts.Items.Add(string.Format("[{0}]", post.Type));
                    }
                }
            }

            if(m_LoggedInUser.Posts.Count == 0)
            {
                MessageBox.Show("No Posts to retrieve :(");
            }
        }

        private void fetchSelectedPosts()
        {
            Post selected = m_LoggedInUser.Posts[listBoxDisplayPosts.SelectedIndex];
            listBoxPostComments.DisplayMember = "Message";
            listBoxPostComments.DataSource = selected.Comments;
        }

        private void fetchCheckins()
        {
            foreach(Checkin checkin in m_LoggedInUser.Checkins)
            {
                if(!listBoxCheckins.Items.Contains(checkin.Place.Name))
                {
                    listBoxCheckins.Items.Add(checkin.Place.Name);
                }
            }

            if(m_LoggedInUser.Checkins.Count == 0)
            {
                MessageBox.Show("No Checkins to retrieve :(");
            }
        }

        private void fetchEvents()
        {
            listBoxEvents.Items.Clear();
            listBoxEvents.DisplayMember = "Name";
            foreach(Event fbEvent in m_LoggedInUser.Events)
            {
                if(!listBoxEvents.Items.Contains(fbEvent))
                {
                    listBoxEvents.Items.Add(fbEvent);
                }
            }

            if(m_LoggedInUser.Events.Count == 0)
            {
                MessageBox.Show("No Events to retrieve :(");
            }
        }

        private void fetchPages()
        {
            listBoxPages.Items.Clear();
            listBoxPages.DisplayMember = "Name";

            foreach(Page page in m_LoggedInUser.LikedPages)
            {
                if(!listBoxPages.Items.Contains(page))
                {
                    listBoxPages.Items.Add(page);
                }
            }

            if(m_LoggedInUser.LikedPages.Count == 0)
            {
                {
                    MessageBox.Show("No liked pages to retrieve :(");
                }
            }
        }
        
        private void buttonSearch_Click(object i_Sender, EventArgs i_E)
        {
            string nameOfAFriendThatHaveMeOnHisFriendsList = textBoxFriendOfAFriendName.Text == "" ? null : textBoxFriendOfAFriendName.Text;

            DateTime bornFromDateTime = dateTimePickerFrom.Value;
            DateTime bornFromDateTo = dateTimePickerTo.Value;

            if(numericUpDownMinNumberFriends != null)
            {
                int minNumberOfFriends = int.Parse(numericUpDownMinNumberFriends.Text);
            }

            if(numericUpDownMaxNumberOfFriends != null)
            {
                int maxNumberOfFriends = int.Parse(numericUpDownMaxNumberOfFriends.Text);
            }



            List<User> friendCollection = FilterByFirstName(m_LoggedInUser.Friends.ToList());
           
            friendCollection = friendCollection.Where(friend => friend.FriendLists.Count > )
            
            
            
            foreach(User friend in friendCollection)
            {
                listBoxMatchPeoples.Items.Add(friend);
            }
        }

        private List<User> filterByFirstName(List<User> i_FriendsList)
        {
            string firstName = textBoxFirstName.Text == "" ? null : textBoxFirstName.Text;
            return i_FriendsList.Where(friend => friend.FirstName.Equals(null) || friend.FirstName.Equals(firstName)).ToList();
        }

        private List<User> filterByLastName(List<User> i_FriendsList)
        {
            string lastName = textBoxLastName.Text == "" ? null : textBoxLastName.Text;
            return i_FriendsList.Where(friend => friend.LastName.Equals(null) || friend.LastName.Equals(lastName)).ToList();
        }

        
        private void checkBoxEnableDate_CheckedChanged(object i_Sender, EventArgs i_E)
        {
            if (checkBoxEnableDate.Checked)
            {
                dateTimePickerFrom.Visible = true;
                dateTimePickerTo.Visible = true;
                labelTextTo.Visible = true;
            }
            else
            {
                dateTimePickerFrom.Visible = false;
                dateTimePickerTo.Visible = false;
                labelTextTo.Visible = false;
            }
        }

        private void numericUpDownMinNumberFriends_ValueChanged(object i_Sender, EventArgs i_E)
        {
            if(numericUpDownMinNumberFriends.Value > numericUpDownMaxNumberOfFriends.Value)
            {
                numericUpDownMaxNumberOfFriends.Value = numericUpDownMinNumberFriends.Value;
            }
        }

        private void numericUpDownMaxNumberOfFriends_ValueChanged(object i_Sender, EventArgs i_E)
        {
            if (numericUpDownMinNumberFriends.Value > numericUpDownMaxNumberOfFriends.Value)
            {
                numericUpDownMinNumberFriends.Value = numericUpDownMaxNumberOfFriends.Value;
            }
        }

        private void linkLabelFetchCheckins_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if(m_LoggedInUser.Checkins.Count != 0)
            {

                dataGridViewCheckins.DataSource = m_LoggedInUser.Checkins;
                dataGridViewCheckins.Columns["PictureURL"].Visible = false;
                dataGridViewCheckins.Columns["Link"].Visible = false;
                dataGridViewCheckins.Columns["Caption"].Visible = false;
                dataGridViewCheckins.Columns["Description"].Visible = false;
                dataGridViewCheckins.Columns["Name"].Visible = false;
                dataGridViewCheckins.Columns["Source"].Visible = false;
                dataGridViewCheckins.Columns["IconURL"].Visible = false;
                dataGridViewCheckins.Columns["ObjectID"].Visible = false;
                //m_LoggedInUser.Checkins[0].TaggedUsers

            }

        }
    }

}
