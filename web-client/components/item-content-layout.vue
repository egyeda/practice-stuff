﻿<template>
  <div>
    <v-row>
      <v-col cols="12" sm="7" lg="6" offset-lg="1" xl="5" offset-xl="2">
        <slot name="content"></slot>
      </v-col>
      <v-col class="d-none d-sm-block" sm="5" lg="4" xl="3">

        <v-sheet class="pa-3 sticky">
          <slot name="item"></slot>
        </v-sheet>
      </v-col>
    </v-row>
    <v-dialog v-model="dialog">
      <template v-slot:activator="{on}">
        <v-btn class="d-sm-none" fab bottom right small fixed v-on="on" color="blue">
          <v-icon>mdi-help-circle-outline</v-icon>
        </v-btn>
      </template>
      <v-sheet class="pa-2" rounded>
        <slot name="item"></slot>
      </v-sheet>
    </v-dialog>
  </div>
</template>

<script>
export default {
  name: "item-content-layout",
  data: () => ({
    dialog: false
  })
}
</script>

<style scoped>

</style>
