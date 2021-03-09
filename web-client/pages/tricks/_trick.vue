<template>
  <div class="d-flex justify-center">

    <div v-if="submissions">
      <div v-for="x in 1">

        <div v-for="s in submissions">
          {{ s.id }} - {{ s.description }} - {{ s.trickId }}
          <div>
            <video width="400" controls :src="`http://localhost:5000/api/videos/${s.video}`"></video>
          </div>
        </div>
      </div>
    </div>

    <div class="mx-2 sticky">
      Tricks: {{ $route.params.trick }}
    </div>

  </div>
</template>

<script>
import {mapState} from "vuex"

export default {
  computed: {
    ...mapState('submissions', ['submissions']),
  },
  async fetch() {
    const trickId = this.$route.params.trick
    await this.$store.dispatch("submissions/fetchSubmissionForTrick", {trickId}, {root: true})
  }
}
</script>

<style scoped>
 .sticky{
   position: sticky;
   position: -webkit-sticky;
   top: 48px;
}
</style>
