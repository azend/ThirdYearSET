/*
* FILE : vrodriguesdiamon_a1.c
* PROJECT : PROG1345 - Assignment #1
* PROGRAMMER : Verdi Rodrigues-Diamond
* FIRST VERSION : 2014-0-01
* DESCRIPTION :
* The functions in this file are used to test
* IPC techniques such as threading, mutexes,
* and barriers
*/

#include <stdlib.h>
#include <stdio.h>
#include <pthread.h>
#include <errno.h>
#include <unistd.h>

pthread_mutex_t sleepMutex = PTHREAD_MUTEX_INITIALIZER;
pthread_barrier_t sleepBarrier;

void * lockSleepUnlock(void * c);
void * sleepLockSleepUnlock(void * c);
void * sleepTryLockSleepUnlock(void * c);

int main(int argc, char *argv[]) {

	printf("Allocating memory for pthread_ts\r\n");
	pthread_t lockSleepUnlockThreadId;
	pthread_t sleepLockSleepUnlockThreadId;
	pthread_t sleepTryLockSleepUnlockThreadId;

	printf("Initializing a pthread barrier to not quit until 3 waits\r\n");
	pthread_barrier_init(&sleepBarrier, NULL, 3);

	printf("Spawning thread for function lockSleepUnlock\r\n");
	pthread_create(&lockSleepUnlockThreadId, NULL, lockSleepUnlock, NULL);
	printf("Spawning thread for function sleepLockSleepUnlock\r\n");
	pthread_create(&sleepLockSleepUnlockThreadId, NULL, sleepLockSleepUnlock, NULL);
	printf("Spawning thread for function sleepTryLockSleepUnlock\r\n");
	pthread_create(&sleepTryLockSleepUnlockThreadId, NULL, sleepTryLockSleepUnlock, NULL);

	printf("Starting process to join lockSleepUnlock thread with the main thread\r\n");
	pthread_join(lockSleepUnlockThreadId, NULL);

	printf("Main thread waiting at the barrier\r\n");
	pthread_barrier_wait(&sleepBarrier);

	//sleep(30);

	return EXIT_SUCCESS;
}

//
// FUNCTION : lockSleepUnlock
// DESCRIPTION :
// This function locks sleepMutex, waits
// for 5 seconds, and unlocks sleepMutex.
// PARAMETERS :
// RETURNS :
//
void * lockSleepUnlock(void * c) {
	printf("Starting lockSleepUnlock\r\n");

	printf("lockSleepUnlock thread waiting for sleepMutex lock\r\n");
	pthread_mutex_lock(&sleepMutex);
	printf("lockSleepUnlock thread acquired lock on sleepMutex\r\n");

	sleep(5);

	printf("lockSleepUnlock finished work and is unlocking sleepmMutex\r\n");
	pthread_mutex_unlock(&sleepMutex);
	printf("lockSleepUnlock has finished unlocking sleepMutex\r\n");

	printf("lockSleepUnlock ending\r\n");

	return NULL;
}

//
// FUNCTION : sleepLockSleepUnlock
// DESCRIPTION :
// This function waits for a second,
// locks sleepMutex, waits for 5
// seconds, unlocks sleepMutex, and
// then waits at the barrier for
// the other threads to finish.
// PARAMETERS :
// RETURNS :
//
void * sleepLockSleepUnlock(void * c) {
	printf("Starting sleepLockSleepUnlock\r\n");

	sleep(1);

	printf("sleepLockSleepUnlock thread waiting for sleepMutex lock\r\n");
	pthread_mutex_lock(&sleepMutex);
	printf("sleepLockSleepUnlock thread acquired lock on sleepMutex\r\n");

	sleep(5);

	printf("sleepLockSleepUnlock finished work and is unlocking sleepmMutex\r\n");
	pthread_mutex_unlock(&sleepMutex);
	printf("sleepLockSleepUnlock has finished unlocking sleepMutex\r\n");

	printf("sleepLockSleepUnlock waiting at the barrier\r\n");
	pthread_barrier_wait(&sleepBarrier);

	printf("sleepLockSleepUnlock ending\r\n");

	return NULL;
}

//
// FUNCTION : sleepLockSleepUnlock
// DESCRIPTION :
// This function waits for 2 seconds,
// tries to lock sleepMutex, waits for 2
// seconds, unlocks sleepMutex, and
// then waits at the barrier for
// the other threads to finish.
// PARAMETERS :
// RETURNS :
//
void * sleepTryLockSleepUnlock(void * c) {
	printf("Starting sleepTryLockSleepUnlock\r\n");

	sleep(2);

	printf("sleepTryLockSleepUnlock thread trying to lock mutex\r\n");
	int res = pthread_mutex_trylock(&sleepMutex);

	if (res == EOK) {
		printf("sleepTryLockSleepUnlock locked sleepMutex\r\n");

		sleep(2);

		pthread_mutex_unlock(&sleepMutex);
	}
	else {
		printf("sleepTryLockSleepUnlock couldn't lock sleepMutex\r\n");
	}

	printf("sleepTryLockSleepUnlock waiting at the barrier\r\n");
	pthread_barrier_wait(&sleepBarrier);

	printf("sleepTryLockSleepUnlock ending\r\n");

	return NULL;
}
