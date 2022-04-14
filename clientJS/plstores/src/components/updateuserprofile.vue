<template>
    <v-card>
        <v-card-title>Edit User-Profile</v-card-title>
        <v-card-text>
             <v-form>
        <v-text-field label="UserName"></v-text-field>
        <v-text-field label="Password"></v-text-field>
    </v-form>
        </v-card-text>
        <v-card-actions>
              <v-btn color="primary">Update</v-btn>
              <v-btn color="primary">Cancel</v-btn>
              <v-btn color="primary">Back to home</v-btn>
        </v-card-actions>
    </v-card>
</template>