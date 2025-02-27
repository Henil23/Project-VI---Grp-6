﻿using JobApplicationPortal.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobApplicationPortal.Repo
{
    public class JobRepo
    {
        private readonly IMongoCollection<Job> _jobsCollection;

        public JobRepo(MongoDbContext context)
        {
            _jobsCollection = context.GetCollection<Job>("Jobs");
        }

        public async Task<List<Job>> GetAllJobsAsync()
        {
            return await _jobsCollection.Find(_ => true).ToListAsync();  // Fetch all jobs
        }

        public async Task<Job> GetJobByIdAsync(string id)
        {
            return await _jobsCollection.Find(job => job.JobID == id).FirstOrDefaultAsync();
        }

        public async Task AddJobAsync(Job job)
        {
            await _jobsCollection.InsertOneAsync(job);  // Save job to MongoDB
        }

        public async Task UpdateJobAsync(string id, Job job)
        {
            await _jobsCollection.ReplaceOneAsync(j => j.JobID == id, job);
        }

        public async Task DeleteJobAsync(string id)
        {
            await _jobsCollection.DeleteOneAsync(job => job.JobID == id);
        }
    }
}
