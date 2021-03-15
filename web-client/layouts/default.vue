<template>
  <v-app dark>
    <v-app-bar app dense>
      <nuxt-link class="text-h5 text--primary" style="text-decoration: none;" :to="'/'">Tricking library</nuxt-link>

      <v-spacer></v-spacer>
      <v-btn to="/moderation" depressed>Moderation</v-btn>

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
export default {}
</script>
