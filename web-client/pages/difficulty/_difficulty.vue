﻿<template>

  <item-content-layout>
    <template v-slot:content>
      <trick-list :tricks="tricks"/>
    </template>
    <template v-slot:item>

      <div>
        <div v-if="difficulty">
          <div class="text-h6">{{ difficulty.name }}</div>
          <v-divider class="my-1"></v-divider>
          <div class="text-body-2">{{ difficulty.description }}</div>
        </div>
      </div>
    </template>
  </item-content-layout>
</template>
<script>
import {mapGetters} from "vuex";

export default {
  name: "difficulty",
  data: () => ({
    difficulty: null,
    tricks: [],
  }),
  computed: mapGetters('tricks', ['difficultyById']),
  async fetch() {
    const difficultyId = this.$route.params.difficulty
    this.difficulty = this.difficultyById(difficultyId);
    this.tricks = await this.$axios.$get(`/api/difficulties/${difficultyId}/tricks`);
  },
  head() {
    if (!this.difficulty) return {};
    return {
      title: this.difficulty.name,
      meta: [
        {
          hid: 'description',
          name: 'description',
          content: this.difficulty.description
        }
      ]
    }
  },
}
</script>

<style scoped>

</style>
