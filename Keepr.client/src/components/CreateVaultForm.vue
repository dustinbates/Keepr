<template>
  <div class="modal fade" id="createVault" data-bs-backdrop="static" tabindex="-1" data-bs-keyboard="false" role="dialog"
    aria-hidden="true">
    <div class="modal-dialog modal-dialog-scrollable modal-dialog-centered modal-md" role="document">
      <div class="modal-content">
        <div class="container-fluid bg-light p-4">
          <span class="d-flex justify-content-between">
            <h2 class="mb-3">Add your vault</h2>
            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
          </span>
          <form class="row" @submit.prevent="createVault">
            <div class="mb-3">
              <input type="text" class="form-control" required v-model="editable.name" maxlength="25" name="name" id=""
                aria-describedby="helpId" placeholder="Title...">
            </div>
            <div class="mb-3">
              <input type="text" class="form-control" required v-model="editable.img" name="img" id=""
                aria-describedby="helpId" placeholder="Image URL...">
            </div>
            <div class="mb-3">
              <input type="text" class="form-control mb-5" required v-model="editable.description" maxlength="300"
                name="description" id="" aria-describedby="helpId" placeholder="Description...">
            </div>
            <div class="d-flex justify-content-end align-items-center">
              <input type="checkbox" class="checkbox me-2" v-model="editable.isPrivate" name="isPrivate" id="checkbox"
                aria-describedBy="helpId">
              <p class="m-0">Make Vault Private?</p>
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
import { useRoute } from 'vue-router';
import { logger } from '../utils/Logger';
import Pop from '../utils/Pop';
import { vaultsService } from '../services/VaultsService.js'

export default {
  setup() {
    const editable = ref({});
    const route = useRoute();
    return {
      editable,
      async createVault() {
        try {
          const profileId = route.params.profileId
          const vaultData = editable.value
          await vaultsService.createVault(vaultData, profileId)
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