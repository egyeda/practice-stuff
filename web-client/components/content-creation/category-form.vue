﻿<template>
  <v-card>
    <v-card-title>
      Create Category
      <v-spacer/>
      <v-btn icon @click="close">
        <v-icon>mdi-close</v-icon>
      </v-btn>
    </v-card-title>
    <v-card-text>
      <v-text-field :label="'Name'" v-model="form.name"></v-text-field>
      <v-text-field :label="'Description'" v-model="form.description"></v-text-field>
    </v-card-text>
    <v-card-actions class="d-flex justify-center">
      <v-btn @click="save">Save</v-btn>
    </v-card-actions>
  </v-card>

</template>

<script>

import {close} from "@/components/content-creation/_shared";

export default {
  name: "category-form",
  data: () => ({
    form: {
      name: "",
      description: "",
    },
  }),
  mixins: [close],
  methods:{
    save(){
      this.$axios.$post("api/categories", this.form);
      this.close()
    }
  }
}
</script>

<style scoped>

</style>
