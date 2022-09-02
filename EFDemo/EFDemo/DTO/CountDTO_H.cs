namespace EFDemo.DTO
{
    public class ReviewedCountDTO_H
    {
        public ReviewedCountDTO_H() { }
        public int vol_apply_notreviewed { get; set; }
        public int vol_apply_reviewed { get; set; }

        public int clue_repo_notreviewed { get; set; }
        public int clue_repo_reviewed { get; set; }

        public int info_repo_notreviewed { get; set; }
        public int info_repo_reviewed { get; set; }

    }
    public class VolApplyDTO_H
    {
        public VolApplyDTO_H() { }
        public int vol_apply_notreviewed { get; set; }
        public int vol_apply_reviewed { get; set; }
    }

    public class ClueRepoDTO_H
    {
        public ClueRepoDTO_H() { }
        public int clue_repo_notreviewed { get; set; }
        public int clue_repo_reviewed { get; set; }
    }

    public class InfoRepoDTO_H
    {
        public InfoRepoDTO_H() { }
        public int info_repo_notreviewed { get; set; }
        public int info_repo_reviewed { get; set; }


    }
}
