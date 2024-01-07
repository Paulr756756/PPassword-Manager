<script setup>
import { ref } from 'vue';
import { auth_getPasswordFromAPI } from '../apiCalls';
import DeleteButton from './DeleteButton.vue';

const emit = defineEmits(['deleted'])

const props = defineProps({
    title: String,
})
const modalRef = ref(props.title)
const password = ref("")
const isLoading = ref(true)

const onButtonClick = async () => {
    const requestBody = {
        'title': props.title
    }
    isLoading.value = true
    if (modalRef.value) modalRef.value.showModal()
    try {
        password.value = await auth_getPasswordFromAPI(requestBody)
        isLoading.value = false
    } catch (error) {
        password.value = 'Error fetching data'
        console.log(error)
        isLoading.value = false
    }
}

const copyPassword = async () => {
    await navigator.clipboard.writeText(model.generatedPwd.value)
}

</script>

<template>
    <div class="card w-96 bg-base-100 shadow-xl">
        <div class="card-body">
            <h2 class="card-title">{{ props.title }}</h2>
            <div class="card-actions justify-end">
                <button class="btn btn-primary" @click="onButtonClick">Generate password</button>
                <dialog ref="modalRef" class="modal modal-bottom sm:modal-middle">
                    <div class="modal-box relative h-40">
                        <span class="absolute w-50 top-1/2 left-1/2 translate-y-[-50%] translate-x-[-50%] loading loading-infinity loading-lg" :class="isLoading?'':'hidden' "></span>
                        <div :class="isLoading?'hidden':'' " class="bg-base-content rounded-lg grid">
                            <div class="col-start-1 row-start-1 text-slate-300 p-2 items-center">
                                {{ password }}
                            </div>
                            <div class="col-start-1 row-start-1 flex items-start justify-end p-2 rtl:justify-start">
                                <div class="tooltip tooltip-left tooltip-accent" data-tip="copy">
                                    <button class="btn btn-square btn-sm btn-neutral" @click.prevent="copyPassword">
                                        <svg class="h-5 w-5 fill-current" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 32 32">
                                            <path d="M 16 3 C 14.742188 3 13.847656 3.890625 13.40625 5 L 6 5 L 6 28 L 26 28 L 26 5 L 18.59375 5 C 18.152344 3.890625 17.257813 3 16 3 Z M 16 5 C 16.554688 5 17 5.445313 17 6 L 17 7 L 20 7 L 20 9 L 12 9 L 12 7 L 15 7 L 15 6 C 15 5.445313 15.445313 5 16 5 Z M 8 7 L 10 7 L 10 11 L 22 11 L 22 7 L 24 7 L 24 26 L 8 26 Z"></path>
                                        </svg>
                                    </button>
                                </div>
                            </div>
                        </div>
                        <div class="modal-action" :class="isLoading?'hidden':'' ">
                            <form method="dialog">
                                <button class="btn">Close</button>
                            </form>
                        </div>
                    </div>
                </dialog>
                <DeleteButton @deleted="emit('deleted')"/>
            </div>
        </div>
    </div>
</template>

<style scoped></style>