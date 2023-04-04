<template>
  <div class="modal fade" id="createKeep" data-bs-backdrop="static" tabindex="-1" data-bs-keyboard="false" role="dialog"
    aria-hidden="true">
    <div class="modal-dialog modal-dialog-scrollable modal-dialog-centered modal-md" role="document">
      <div class="modal-content">
        <div class="container-fluid bg-light p-4">
          <span class="d-flex justify-content-between">
            <h2 class="mb-3">Add your keep</h2>
            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
          </span>
          <form class="row" @submit.prevent="createKeep">
            <div class="mb-3">
              <input type="text" class="form-control" required v-model="editable.name" name="name" id=""
                aria-describedby="helpId" placeholder="Title...">
            </div>
            <div class="mb-3">
              <input type="text" class="form-control mb-5" required v-model="editable.img" name="img" id=""
                aria-describedby="helpId" placeholder="Image URL...">
            </div>
            <div class="mb-3">
              <input type="text" class="form-control mt-5" required v-model="editable.description" name="description"
                id="" aria-describedby="helpId" placeholder="Keep Description...">
            </div>
            <div class="text-end">
              <button type="submit" class="btn btn-dark" data-bs-dismiss="modal">Create</button>
            </div>
          </form>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { ref } from 'vue';
import { keepsService } from '../services/KeepsService';
import { logger } from '../utils/Logger';
import Pop from '../utils/Pop';

export default {
  setup() {
    const editable = ref({})
    return {
      editable,
      async createKeep() {
        try {
          const keepData = editable.value
          await keepsService.createKeep(keepData)
          editable.value = {}
        } catch (error) {
          logger.error(error)
          Pop.error(error.message)
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>
input {
  padding: 0 !important;
  background: none;
  height: 2em;
  border: none;
  border-bottom: 1px solid black;
  border-radius: 0 !important;
}
</style>