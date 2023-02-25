public IDataResult<StudyPredispositionDto> CalculateStudyPredisposition(DateTime date) // Tested
{
    var result = GetStudyPeriodsByDateRange(FindFirstStudyPeriodRecordDate(), date);
    double totalMinutesOfStartingTimes = 0;
    double totalMinutesOfEndingTimes = 0;

    foreach (var item in result.Data)
    {
        totalMinutesOfStartingTimes += item.StartingTime.TotalMinutes;
        totalMinutesOfEndingTimes += item.EndTime.TotalMinutes;
    }

    return new SuccessDataResult<StudyPredispositionDto>(new StudyPredispositionDto
    {
        AverageStartTime = TimeSpan.FromMinutes(totalMinutesOfStartingTimes / result.Data.Count),
        AverageEndTime = TimeSpan.FromMinutes(totalMinutesOfEndingTimes / result.Data.Count)
    });
}

private void FitTimeRange(List<StudyPeriod> studyPeriods)
{
    List<StudyPeriod> gonnaSlice = new List<StudyPeriod>();
    List<StudyPeriod> morningRange = new List<StudyPeriod>();
    List<StudyPeriod> noonRange = new List<StudyPeriod>();
    List<StudyPeriod> afternoonRange = new List<StudyPeriod>();
    

    for (int i = 0; i < 2; i++)
    {
        foreach (var item in studyPeriods)
        {
            if (item.StartingTime > new TimeSpan(6, 0, 0) && item.EndTime < new TimeSpan(11, 59, 0))
            {
                morningRange.Add(item);
            }
            else if (item.StartingTime > new TimeSpan(12, 0, 0) && item.EndTime < new TimeSpan(17, 59, 0))
            {
                noonRange.Add(item);
            }
            else if (item.StartingTime > new TimeSpan(18, 0, 0) && item.EndTime < new TimeSpan(23, 59, 0))
            {
                afternoonRange.Add(item);
            }
            else
            {
                gonnaSlice.Add(item);
            }
        }

        for (int j = 0; j < gonnaSlice.Count; j++)
        {

        }
    }
}

private List<StudyPeriod> SliceStudyPeriods(StudyPeriod studyPeriod)
{
    List<StudyPeriod> slicedPeriods = new List<StudyPeriod>();

    if (studyPeriod.StartingTime > new TimeSpan(6, 0, 0))
    {
        if (studyPeriod.EndTime < new TimeSpan(17, 59, 0))
        {
            slicedPeriods.Add(new StudyPeriod
            {
                Id = studyPeriod.Id,
                StartingTime = new TimeSpan(12, 0, 0),
                EndTime = studyPeriod.EndTime,
            });
        }
        else if (studyPeriod.EndTime < new TimeSpan(23, 59, 0))
        {
            slicedPeriods.Add(new StudyPeriod
            {
                Id = studyPeriod.Id,
                StartingTime = new TimeSpan(18, 0, 0),
                EndTime = studyPeriod.EndTime,
            });
        }
    }
    else if (studyPeriod.StartingTime > new TimeSpan(12, 0, 0) && studyPeriod.EndTime < new TimeSpan(23, 59, 0))
    {
        slicedPeriods.Add(new StudyPeriod
        {
            Id = studyPeriod.Id,
            StartingTime = new TimeSpan(17, 0, 0),
            EndTime = studyPeriod.EndTime,
        });
    }

    return slicedPeriods;
}