<template>
  <v-app dark>
    <v-app-bar app dense>
      <v-toolbar-title>Tricking library</v-toolbar-title>

      <v-spacer></v-spacer>
      <content-creation-dialog></content-creation-dialog>
    </v-app-bar>

    <v-main>
      <v-container>
        <nuxt/>
      </v-container>
    </v-main>
  </v-app>
</template>

<script>
export default {
}
</script>
