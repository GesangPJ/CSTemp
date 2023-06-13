 // You need this boolean
 private bool switchingToAnotherForm = false;
 
 // Button function
 private void ButtonName_Click(object sender, EventArgs e)
        {
            switchingToAnotherForm = true;
            // Create an instance of your target form
            TargetForm targetForm = new TargetForm();

            // Hide current form
            this.Hide();

            //Show target Form using ShowDialog (Becase if you use Show() only, it will be bugged)
            targetForm.ShowDialog();

            // Close the current form
            this.Close();
        }