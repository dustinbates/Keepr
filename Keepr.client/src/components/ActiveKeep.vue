<template>
  <div class="modal fade" id="activeKeep" tabindex="-1" data-bs-keyboard="false" role="dialog" aria-hidden="true">
    <div class="modal-dialog modal-dialog-scrollable modal-dialog-centered modal-xl" role="document">
      <div class="modal-content">
        <div class="row">
          <div class="col-6">
            <img :src="keep.img" :alt="keep.name">
          </div>
          <div class="col-6">
            <div class="row h-100 align-items-evenly">
              <div class="col-12 text-center mt-2">
                <p class="fs-2 m-0 p-0">
                  <i class="mdi mdi-eye-outline" title="View count"></i> {{ keep.views }}
                  <i class="mdi mdi-alpha-k-box-outline" title="Kept count"></i> {{ keep.kept }}
                </p>
              </div>
              <div class="row">
                <div class="col-12">
                  <h1 class="text-center">{{ keep.name }}</h1>
                  <p class="text-start">{{ keep.description }}</p>
                </div>
              </div>
              <div class="row">
                <div class="col-6">

                </div>
                <div class="col-6">

                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { computed } from 'vue';
import { AppState } from '../AppState';
export default {
  setup() {
    return {
      keep: computed(() => AppState.activeKeep),
    }
  }
}
</script>


<style lang="scss" scoped>
img {
  height: 50vh;
  width: 100%;
  object-fit: cover;
}
</style>